namespace VDew.DomainDrivenDesign.Infrastructure.EventsStorage
{
    /// <summary>
    /// Contract to be implemented by an event store, i.e. a store specialized in saving and retrieving <see cref="Domain.Events.IDomainEvent{TKey}"/>, serialized as <see cref="EventData"/>.
    /// </summary>
    public interface IEventStore : IDisposable
    {
        /// <summary>
        /// Asynchronously saves the specified <paramref name="eventData"/>.
        /// </summary>
        /// <param name="eventData">Event stored as <see cref="EventData"/>.</param>
        /// <param name="cancellationToken">Optional instance of <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task AddEventAsync(EventData eventData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves all stored events that happened to an aggregate specified by the string representation of its unique identifier.
        /// </summary>
        /// <param name="aggregateRootId">String representation of the unique idenfitier of the <see cref="Domain.IAggregateRoot{TKey}"/>.</param>
        /// <param name="cancellationToken">Optional instance of <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="List{T}"/>containing the <see cref="EventData"/>.</returns>
        Task<List<EventData>> GetEventsListAsync(string aggregateRootId, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously begins a transaction against the underlying storage.
        /// </summary>        
        /// <param name="cancellationToken">Optional instance of <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>An instance of <see cref="ITransaction"/>.</returns>
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously saves the current changes to the uderlying storage.
        /// </summary>
        /// <param name="cancellationToken">Optional instance of <see cref="CancellationToken"/> to cancel the current operation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
