using System;
using System.Collections.Concurrent;
using LoanProcessingApp.Models;

namespace LoanProcessingApp.Repositories
{
    /// <summary>
    /// Simple in-memory store shared across services. Suitable for demo/testing purposes;
    /// replace with a persistent data store (e.g., EF Core + database) for production use.
    /// </summary>
    public interface ILoanProcessingRepository
    {
        ConcurrentDictionary<Guid, Lender> Lenders { get; }

        ConcurrentDictionary<Guid, Borrower> Borrowers { get; }

        ConcurrentDictionary<Guid, LoanBooking> LoanBookings { get; }
    }

    public class LoanProcessingRepository : ILoanProcessingRepository
    {
        public ConcurrentDictionary<Guid, Lender> Lenders { get; } = new();

        public ConcurrentDictionary<Guid, Borrower> Borrowers { get; } = new();

        public ConcurrentDictionary<Guid, LoanBooking> LoanBookings { get; } = new();
    }
}
