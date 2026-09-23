using System;
using System.ComponentModel.DataAnnotations;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Request payload used by a loan approver to approve or reject a loan booking.
    /// </summary>
    public class LoanApprovalRequest
    {
        [Required(ErrorMessage = "Loan booking ID is required.")]
        public Guid LoanBookingId { get; set; }

        [Required(ErrorMessage = "Approval decision is required.")]
        public bool Approve { get; set; }

        public string? Comments { get; set; }
    }
}
