using System.ComponentModel.DataAnnotations;

namespace LoanProcessingApp.Dtos
{
    /// <summary>
    /// Request payload for registering a new lender.
    /// </summary>
    public class LenderRegistrationRequest
    {
        [Required(ErrorMessage = "Lender name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lender email is required.")]
        [EmailAddress(ErrorMessage = "Lender email must be a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Available amount must be greater than zero.")]
        public decimal AvailableAmount { get; set; }

        [Range(0.01, 100, ErrorMessage = "Interest rate must be between 0.01 and 100.")]
        public decimal InterestRate { get; set; }
    }
}
