namespace LoanProcessingApp.Models
{
    /// <summary>
    /// Strategy for selecting which registered lender funds a loan booking.
    /// </summary>
    public enum LenderAllocationStrategy
    {
        /// <summary>First registered lender with sufficient available funds.</summary>
        FirstOut,

        /// <summary>Most recently registered lender with sufficient available funds.</summary>
        LastOut
    }
}
