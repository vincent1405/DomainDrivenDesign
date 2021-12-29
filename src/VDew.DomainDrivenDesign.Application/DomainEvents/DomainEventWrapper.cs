using System.Text.Json.Serialization;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Application.DomainEvents
{
    namespace Europorte.Framework.DomainDrivenDesign.Application.DomainEvents
    {
        /// <summary>
        /// Class to implement the <see cref="IDomainEventWrapper{TEventType, TKey}"/> contract.
        /// </summary>
        /// <typeparam name="TEventType">Type of the <see cref="IDomainEvent{TKey}"/>.</typeparam>
        /// <typeparam name="TKey">Type of the identifier of the entity/aggregate root.</typeparam>
        public class DomainEventWrapper<TEventType, TKey> : IDomainEventWrapper<TEventType, TKey> where TEventType : IDomainEvent<TKey>
        {
            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            [JsonIgnore]
            public TEventType DomainEvent { get; }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            public Guid EventId { get; }

            /// <summary>
            /// Initialize a new instance of <see cref="DomainEventWrapper{TEventType, TKey}"/>.
            /// </summary>
            /// <param name="domainEvent">Instance of <typeparamref name="TEventType"/> to send notification from.</param>
            public DomainEventWrapper(TEventType domainEvent)
            {
                EventId = Guid.NewGuid();
                DomainEvent = domainEvent;
            }
        }
    }
}
