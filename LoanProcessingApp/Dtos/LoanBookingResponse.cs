using System;
using LoanProcessingApp.Models;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Response payload representing a booked loan.
    /// </summary>
    public class LoanBookingResponse
    {
        public Guid Id { get; set; }

        public Guid BorrowerId { get; set; }

        public Guid LenderId { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public LenderAllocationStrategy AllocationStrategy { get; set; }

        public LoanApprovalStatus ApprovalStatus { get; set; }

        public string? ApprovalComments { get; set; }

        public DateTime BookedDate { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
