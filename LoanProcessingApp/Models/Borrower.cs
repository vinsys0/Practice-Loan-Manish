using System;

namespace LoanProcessingApp.Models
{
    /// <summary>
    /// Represents a registered borrower who can apply for and receive loan bookings.
    /// </summary>
    public class Borrower
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
