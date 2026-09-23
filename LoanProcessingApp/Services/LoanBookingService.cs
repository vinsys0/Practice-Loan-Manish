using System;
using System.Collections.Generic;
using System.Linq;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Models;
using LoanProcessingApp.Repositories;

namespace LoanProcessingApp.Services
{
    public interface ILoanBookingService
    {
        LoanBookingResponse Book(LoanBookingRequest request);

        LoanBookingResponse? GetById(Guid id);

        IReadOnlyList<LoanBookingResponse> GetAll();
    }

    /// <summary>
    /// Books loans against registered borrowers, allocating funds from a registered
    /// lender selected using a First-Out (earliest registered) or Last-Out
    /// (most recently registered) strategy, and applies the lender's interest rate.
    /// </summary>
    public class LoanBookingService : ILoanBookingService
    {
        private readonly ILoanProcessingRepository _repository;

        public LoanBookingService(ILoanProcessingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public LoanBookingResponse Book(LoanBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!_repository.Borrowers.ContainsKey(request.BorrowerId))
            {
                throw new InvalidOperationException($"Borrower '{request.BorrowerId}' is not registered.");
            }

            var eligibleLenders = _repository.Lenders.Values
                .Where(l => l.AvailableAmount >= request.PrincipalAmount);

            var lender = request.AllocationStrategy == LenderAllocationStrategy.FirstOut
                ? eligibleLenders.OrderBy(l => l.RegisteredDate).FirstOrDefault()
                : eligibleLenders.OrderByDescending(l => l.RegisteredDate).FirstOrDefault();

            if (lender == null)
            {
                throw new InvalidOperationException("No lender with sufficient available funds could be found.");
            }

            lender.AvailableAmount -= request.PrincipalAmount;

            var booking = new LoanBooking
            {
                Id = Guid.NewGuid(),
                BorrowerId = request.BorrowerId,
                LenderId = lender.Id,
                PrincipalAmount = request.PrincipalAmount,
                InterestRate = lender.InterestRate,
                TenureMonths = request.TenureMonths,
                AllocationStrategy = request.AllocationStrategy,
                ApprovalStatus = LoanApprovalStatus.PendingApproval,
                BookedDate = DateTime.UtcNow
            };

            _repository.LoanBookings[booking.Id] = booking;

            return ToResponse(booking);
        }

        public LoanBookingResponse? GetById(Guid id)
        {
            return _repository.LoanBookings.TryGetValue(id, out var booking) ? ToResponse(booking) : null;
        }

        public IReadOnlyList<LoanBookingResponse> GetAll()
        {
            return _repository.LoanBookings.Values
                .OrderBy(b => b.BookedDate)
                .Select(ToResponse)
                .ToList();
        }

        private static LoanBookingResponse ToResponse(LoanBooking booking)
        {
            return new LoanBookingResponse
            {
                Id = booking.Id,
                BorrowerId = booking.BorrowerId,
                LenderId = booking.LenderId,
                PrincipalAmount = booking.PrincipalAmount,
                InterestRate = booking.InterestRate,
                TenureMonths = booking.TenureMonths,
                AllocationStrategy = booking.AllocationStrategy,
                ApprovalStatus = booking.ApprovalStatus,
                ApprovalComments = booking.ApprovalComments,
                BookedDate = booking.BookedDate,
                ApprovalDate = booking.ApprovalDate
            };
        }
    }
}
