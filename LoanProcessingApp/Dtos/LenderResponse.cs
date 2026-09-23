using System;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Response payload representing a registered lender.
    /// </summary>
    public class LenderResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal AvailableAmount { get; set; }

        public decimal InterestRate { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
