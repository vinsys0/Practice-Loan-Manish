using System;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Response payload representing a registered borrower.
    /// </summary>
    public class BorrowerResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
