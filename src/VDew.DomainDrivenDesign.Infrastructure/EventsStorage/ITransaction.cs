namespace VDew.DomainDrivenDesign.Infrastructure.EventsStorage
{
    /// <summary>
    /// Contract to be implemented by a database transaction.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Asynchronously commits the current transaction to the database.
        /// </summary>
        /// <param name="cancellationToken">Optional <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously rollbacks the current transaction to the database.
        /// </summary>
        /// <param name="cancellationToken">Optional <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}