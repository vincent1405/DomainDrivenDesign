using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Application.DomainEvents
{
    /// <summary>
    /// Base class to implement the <see cref="IDomainEventNotification{TEventType, TKey}"/> contract.
    /// </summary>
    /// <typeparam name="TEventType">Type of the <see cref="IDomainEvent{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the identifier of the entity/aggregate root.</typeparam>
    public class DomainNotificationBase<TEventType, TKey> : IDomainEventNotification<TEventType, TKey> where TEventType : IDomainEvent<TKey>
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
        /// Initialize a new instance of <see cref="DomainEventBase{TAggregateRoot, TKey}"/>.
        /// </summary>
        /// <param name="domainEvent">Instance of <typeparamref name="TEventType"/> to send notification from.</param>
        public DomainNotificationBase(TEventType domainEvent)
        {
            DomainEvent = domainEvent;
            EventId = Guid.NewGuid();
        }
    }
}
