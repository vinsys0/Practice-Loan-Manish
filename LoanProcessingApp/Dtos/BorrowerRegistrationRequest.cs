using System.ComponentModel.DataAnnotations;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Request payload for registering a new borrower.
    /// </summary>
    public class BorrowerRegistrationRequest
    {
        [Required(ErrorMessage = "Borrower name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Borrower email is required.")]
        [EmailAddress(ErrorMessage = "Borrower email must be a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Annual income must be greater than zero.")]
        public decimal AnnualIncome { get; set; }
    }
}
