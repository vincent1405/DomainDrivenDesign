namespace VDew.DomainDrivenDesign.Domain.Validation
{
    /// <summary>
    /// Contract to be implemented by a synchronous business Rule.
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// Get the error message to display when the business rule is broken.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Synchronously tests if the business rule is broken (returns true) or not broken (returns false).
        /// </summary>
        /// <returns>True if the rule is broken (business rule is not respected) or false if the business rule is respected.</returns>
        bool IsBroken();
    }
}
