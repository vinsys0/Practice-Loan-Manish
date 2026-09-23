using System;

namespace LoanProcessingApp.Models
{
    public enum LoanApprovalStatus
    {
        PendingApproval,
        Approved,
        Rejected
    }

    /// <summary>
    /// Represents a loan booking created against a borrower and funded by an allocated lender.
    /// </summary>
    public class LoanBooking
    {
        public Guid Id { get; set; }

        public Guid BorrowerId { get; set; }

        public Guid LenderId { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public LenderAllocationStrategy AllocationStrategy { get; set; }

        public LoanApprovalStatus ApprovalStatus { get; set; } = LoanApprovalStatus.PendingApproval;

        public string? ApprovalComments { get; set; }

        public DateTime BookedDate { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
