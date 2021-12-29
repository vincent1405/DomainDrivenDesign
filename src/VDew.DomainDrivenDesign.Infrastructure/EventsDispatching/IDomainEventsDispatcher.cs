using VDew.DomainDrivenDesign.Domain;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsDispatching
{
    /// <summary>
    /// Contract to be implemented by a domain events dispatcher.
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of the <see cref="IAggregateRoot{TKey}"/> whose domain events come from.</typeparam>
    /// <typeparam name="TKey">Type of the <typeparamref name="TAggregateRoot"/> key.</typeparam>
    public interface IDomainEventsDispatcher<TAggregateRoot, TKey> where TAggregateRoot : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Asynchronously dispatches the <see cref="IDomainEvent{TKey}"/> that happened to the specified <paramref name="aggregateRoot"/>.
        /// </summary>
        /// <param name="aggregateRoot">The <paramref name="aggregateRoot"/> which domain events happened on.</param>
        /// <param name="cancellationToken">Optional instance of <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task DispatchDomainEventsAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    }
}
