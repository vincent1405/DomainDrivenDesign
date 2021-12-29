namespace VDew.DomainDrivenDesign.Domain.Validation
{
    /// <summary>
    /// Contract to be implemented by an asynchronous business Rule.
    /// </summary>
    public interface IAsyncBusinessRule
    {
        /// <summary>
        /// Asynchronously tests if the business rule is broken (returns true) or not broken (returns false).
        /// <paramref name="cancellationToken">Token to indicate the task must be canceled.</paramref>
        /// </summary>
        /// <returns>True if the rule is broken (business rule is not respected) or false if the business rule is respected.</returns>
        Task<bool> IsBrokenAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get the error message to display when the business rule is broken.
        /// </summary>
        string Message { get; }
    }
}
