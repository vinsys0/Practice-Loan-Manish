using System;
using System.Collections.Generic;
using System.Linq;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Models;
using LoanProcessingApp.Repositories;

namespace LoanProcessingApp.Services
{
    public interface IBorrowerService
    {
        BorrowerResponse Register(BorrowerRegistrationRequest request);

        BorrowerResponse? GetById(Guid id);

        IReadOnlyList<BorrowerResponse> GetAll();
    }

    /// <summary>
    /// Manages borrower registration.
    /// </summary>
    public class BorrowerService : IBorrowerService
    {
        private readonly ILoanProcessingRepository _repository;

        public BorrowerService(ILoanProcessingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public BorrowerResponse Register(BorrowerRegistrationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var borrower = new Borrower
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                AnnualIncome = request.AnnualIncome,
                RegisteredDate = DateTime.UtcNow
            };

            _repository.Borrowers[borrower.Id] = borrower;

            return ToResponse(borrower);
        }

        public BorrowerResponse? GetById(Guid id)
        {
            return _repository.Borrowers.TryGetValue(id, out var borrower) ? ToResponse(borrower) : null;
        }

        public IReadOnlyList<BorrowerResponse> GetAll()
        {
            return _repository.Borrowers.Values
                .OrderBy(b => b.RegisteredDate)
                .Select(ToResponse)
                .ToList();
        }

        private static BorrowerResponse ToResponse(Borrower borrower)
        {
            return new BorrowerResponse
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
                AnnualIncome = borrower.AnnualIncome,
                RegisteredDate = borrower.RegisteredDate
            };
        }
    }
}
