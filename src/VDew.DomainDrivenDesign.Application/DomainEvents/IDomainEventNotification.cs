using MediatR;
using System.Diagnostics.CodeAnalysis;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Application.DomainEvents
{
    /// <summary>
    /// Contract to be implemented by any integration event, based upon a domain event.
    /// </summary>
    public interface IDomainEventNotification : INotification
    {
        /// <summary>
        /// Get the identifier of the associated <see cref="IDomainEvent{TKey}"/>.
        /// </summary>
        Guid EventId { get; }
    }

    /// <summary>
    /// Contract to be implemented by any integration event, based upon a domain event.
    /// </summary>
    public interface IDomainEventNotification<out TEventType, TKey> : IDomainEventNotification where TEventType : IDomainEvent<TKey>
    {
        /// <summary>
        /// Get the associated <see cref="IDomainEvent{TKey}"/>.
        /// </summary>
        TEventType DomainEvent { get; }
    }
}
