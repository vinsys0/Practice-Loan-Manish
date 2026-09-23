using System;
using System.Collections.Generic;
using System.Linq;
using LoanProcessingApp.Dtos;
using LoanProcessingApp.Models;
using LoanProcessingApp.Repositories;

namespace LoanProcessingApp.Services
{
    public interface ILenderService
    {
        LenderResponse Register(LenderRegistrationRequest request);

        LenderResponse? GetById(Guid id);

        IReadOnlyList<LenderResponse> GetAll();
    }

    /// <summary>
    /// Manages lender registration.
    /// </summary>
    public class LenderService : ILenderService
    {
        private readonly ILoanProcessingRepository _repository;

        public LenderService(ILoanProcessingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public LenderResponse Register(LenderRegistrationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var lender = new Lender
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                AvailableAmount = request.AvailableAmount,
                InterestRate = request.InterestRate,
                RegisteredDate = DateTime.UtcNow
            };

            _repository.Lenders[lender.Id] = lender;

            return ToResponse(lender);
        }

        public LenderResponse? GetById(Guid id)
        {
            return _repository.Lenders.TryGetValue(id, out var lender) ? ToResponse(lender) : null;
        }

        public IReadOnlyList<LenderResponse> GetAll()
        {
            return _repository.Lenders.Values
                .OrderBy(l => l.RegisteredDate)
                .Select(ToResponse)
                .ToList();
        }

        private static LenderResponse ToResponse(Lender lender)
        {
            return new LenderResponse
            {
                Id = lender.Id,
                Name = lender.Name,
                Email = lender.Email,
                AvailableAmount = lender.AvailableAmount,
                InterestRate = lender.InterestRate,
                RegisteredDate = lender.RegisteredDate
            };
        }
    }
}
