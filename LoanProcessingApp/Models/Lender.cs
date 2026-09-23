using System;

namespace LoanProcessingApp.Models
{
    /// <summary>
    /// Represents a registered lender who can fund loan bookings.
    /// </summary>
    public class Lender
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal AvailableAmount { get; set; }

        public decimal InterestRate { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
