using MediatR;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Application.DomainEvents
{
    /// <summary>
    /// Utility interface to be able to create a notification on domain events, without having to reference <see cref="MediatR"/> in the domain assembly.
    /// </summary>
    public interface IDomainEventWrapper : INotification
    {
        /// <summary>
        /// Get the identifier of the associated <see cref="IDomainEvent{TKey}"/>.
        /// </summary>
        Guid EventId { get; }
    }

    /// <summary>
    /// Utility interface to be able to create a notification on domain events, without having to reference <see cref="MediatR"/> in the domain assembly.
    /// </summary>
    public interface IDomainEventWrapper<out TEventType, TKey> : IDomainEventWrapper where TEventType : IDomainEvent<TKey>
    {
        /// <summary>
        /// Get the associated <see cref="IDomainEvent{TKey}"/>.
        /// </summary>
        TEventType DomainEvent { get; }
    }
}
