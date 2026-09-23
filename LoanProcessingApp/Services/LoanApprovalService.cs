using System;
using System.Collections.Generic;
using System.Linq;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Models;
using LoanProcessingApp.Repositories;

namespace LoanProcessingApp.Services
{
    public interface ILoanApprovalService
    {
        LoanBookingResponse Decide(LoanApprovalRequest request);

        IReadOnlyList<LoanBookingResponse> GetPendingApprovals();
    }

    /// <summary>
    /// Allows a loan approver to review pending loan bookings and approve or reject them.
    /// </summary>
    public class LoanApprovalService : ILoanApprovalService
    {
        private readonly ILoanProcessingRepository _repository;

        public LoanApprovalService(ILoanProcessingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public LoanBookingResponse Decide(LoanApprovalRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!_repository.LoanBookings.TryGetValue(request.LoanBookingId, out var booking))
            {
                throw new InvalidOperationException($"Loan booking '{request.LoanBookingId}' was not found.");
            }

            if (booking.ApprovalStatus != LoanApprovalStatus.PendingApproval)
            {
                throw new InvalidOperationException(
                    $"Loan booking '{request.LoanBookingId}' has already been {booking.ApprovalStatus}.");
            }

            booking.ApprovalStatus = request.Approve ? LoanApprovalStatus.Approved : LoanApprovalStatus.Rejected;
            booking.ApprovalComments = request.Comments;
            booking.ApprovalDate = DateTime.UtcNow;

            if (!request.Approve && _repository.Lenders.TryGetValue(booking.LenderId, out var lender))
            {
                // Return funds to the lender if the loan booking is rejected.
                lender.AvailableAmount += booking.PrincipalAmount;
            }

            return ToResponse(booking);
        }

        public IReadOnlyList<LoanBookingResponse> GetPendingApprovals()
        {
            return _repository.LoanBookings.Values
                .Where(b => b.ApprovalStatus == LoanApprovalStatus.PendingApproval)
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
