using VDew.DomainDrivenDesign.Domain;

namespace VDew.DomainDrivenDesign.Application
{
    /// <summary>
    /// Contract to be implemented to implement the Unit Of Work pattern.
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of the aggregate root.</typeparam>
    /// <typeparam name="TKey">Type of the aggregate root identifier.</typeparam>
    public interface IUnitOfWork<TAggregateRoot, TKey> where TAggregateRoot : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Asynchronously commits the operations made on the specified <paramref name="aggregateRoot"/>.
        /// </summary>
        /// <param name="aggregateRoot">Instance of <typeparamref name="TAggregateRoot"/>.</param>
        /// <param name="cancellationToken">Optional <see cref="CancellationToken"/> to notify for task cancellation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task CommitAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    }
}
