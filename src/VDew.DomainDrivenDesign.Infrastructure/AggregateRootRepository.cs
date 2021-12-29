using System.Diagnostics.CodeAnalysis;
using VDew.DomainDrivenDesign.Application;
using VDew.DomainDrivenDesign.Domain;
using VDew.DomainDrivenDesign.Domain.Events;
using VDew.DomainDrivenDesign.Infrastructure.EventsStorage;
using VDew.DomainDrivenDesign.Infrastructure.Serialization;

namespace VDew.DomainDrivenDesign.Infrastructure
{
    /// <summary>
    /// Implementation of the <see cref="IAggregateRootRepository{TAggregateRoot, TKey}"/> based upon a <see cref="IEventStore"/> to store events and a <see cref="IEventSerializer"/> to serialize/deserialize events.
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of the aggregate root.</typeparam>
    /// <typeparam name="TKey">Type of the aggregate root key.</typeparam>
    public class AggregateRootRepository<TAggregateRoot, TKey> : IAggregateRootRepository<TAggregateRoot, TKey> where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private readonly IEventStore eventStoreContext;
        private readonly string aggregateTypeName;
        private readonly IEventSerializer eventSerializer;

        /// <summary>
        /// Initialize a new instance of <see cref="AggregateRootRepository{TAggregateRoot, TKey}"/> with the specified event store and event serializer.
        /// </summary>
        /// <param name="eventStore">Instance of <see cref="IEventStore"/> to store events that happened to <typeparamref name="TAggregateRoot"/> instances.</param>
        /// <param name="eventSerializer">Instance of <see cref="IEventSerializer"/> to serialize/deserialize events that happened to <typeparamref name="TAggregateRoot"/> instances.</param>
        public AggregateRootRepository(IEventStore eventStore, IEventSerializer eventSerializer)
        {
            aggregateTypeName = typeof(TAggregateRoot).Name;
            this.eventSerializer = eventSerializer ?? throw new ArgumentNullException(nameof(eventSerializer));
            this.eventStoreContext = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        private string GetAggreggateRootId(TKey aggregateRootKey)
        {
            return $"{aggregateTypeName}_{aggregateRootKey}";
        }

        private EventData MapDomainEventToEventData(IDomainEvent<TKey> evt, string aggregateId, int aggregateVersion, int payLoadSchemaVersion = 1)
        {
            return new EventData
            {
                AggregateId = aggregateId,
                AggregateVersion = aggregateVersion,
                EventId = Guid.NewGuid(),
                EventType = evt.GetType().FullName!,
                OccurredOn = evt.OccurredOn,
                PayLoad = eventSerializer.Serialize(evt),
                PayLoadSchemaVersion = payLoadSchemaVersion
            };
        }

        [return: NotNull]
        private IDomainEvent<TKey> MapEventDataToDomainEvent([DisallowNull]EventData eventData)
        {
            return eventSerializer.Deserialize<TKey>(eventData.PayLoad, eventData.EventType);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRoot"><inheritdoc/></param>
        /// <param name="cancellationToken"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task AppendAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            if (aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            if (!aggregateRoot.Events.Any())
            {
                return;
            }

            var firstEvent = aggregateRoot.Events.First();
            string aggregateId = GetAggreggateRootId(aggregateRoot.Id);
            var version = firstEvent.AggregateRootVersion - 1;

            foreach (IDomainEvent<TKey>? evt in aggregateRoot.Events)
            {
                var eventData = MapDomainEventToEventData(evt, aggregateId, ++version);
                await eventStoreContext.AddEventAsync(eventData, cancellationToken);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRootKey"><inheritdoc/></param>
        /// <param name="cancellationToken"><inheritdoc/></param>
        /// <returns></returns>
        public async Task<TAggregateRoot?> GetByIdAsync(TKey aggregateRootKey, CancellationToken cancellationToken = default)
        {
            if (aggregateRootKey == null)
            {
                throw new ArgumentNullException(nameof(aggregateRootKey));
            }

            var id = GetAggreggateRootId(aggregateRootKey);
            var events = await eventStoreContext.GetEventsListAsync(id, cancellationToken);
            if (!events.Any())
            {
                return null;
            }

            return AggregateRootBase<TAggregateRoot, TKey>.Create(events.Select(e => MapEventDataToDomainEvent(e)));
        }
    }
}