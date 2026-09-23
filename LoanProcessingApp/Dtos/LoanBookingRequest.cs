using System;
using System.ComponentModel.DataAnnotations;
using LoanProcessingApp.Models;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Request payload for booking a loan against a registered borrower,
    /// funded by a lender selected using the specified allocation strategy.
    /// </summary>
    public class LoanBookingRequest
    {
        [Required(ErrorMessage = "Borrower ID is required.")]
        public Guid BorrowerId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Principal amount must be greater than zero.")]
        public decimal PrincipalAmount { get; set; }

        [Range(1, 480, ErrorMessage = "Tenure must be between 1 and 480 months.")]
        public int TenureMonths { get; set; }

        /// <summary>
        /// Determines whether the first registered or the most recently registered
        /// lender with sufficient funds is used to book the loan.
        /// </summary>
        public LenderAllocationStrategy AllocationStrategy { get; set; } = LenderAllocationStrategy.FirstOut;
    }
}
