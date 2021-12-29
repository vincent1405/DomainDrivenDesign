using VDew.DomainDrivenDesign.Application.DomainEvents;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsDispatching
{
    /// <summary>
    /// Contract to be implemented by a resolver for <see cref="IDomainEventWrapper{TEventType, TKey}"/> and <see cref="IDomainEventNotification{TEventType, TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the aggregate key the domain events are attached to.</typeparam>
    public interface IDomainEventResolver<TKey>
    {
        /// <summary>
        /// Returns a new instance of <see cref="IDomainEventWrapper{TEventType, TKey}"/> according to the specified <paramref name="domainEventWrapperWithGenericType"/> with a constructor that takes the specified <paramref name="domainEvent"/>.
        /// </summary>
        /// <param name="domainEventWrapperWithGenericType">Type that must implement <see cref="IDomainEventWrapper{TEventType, TKey}"/>.</param>
        /// <param name="domainEvent">Instance of <see cref="IDomainEvent{TKey}"/> to pass to the <paramref name="domainEventWrapperWithGenericType"/> constructor.</param>
        /// <returns>An instance of <paramref name="domainEventWrapperWithGenericType"/>, or null if no type is found.</returns>
        IDomainEventWrapper<IDomainEvent<TKey>, TKey> GetDomainEventWrapper(Type domainEventWrapperWithGenericType, IDomainEvent<TKey> domainEvent);

        /// <summary>
        /// Returns a new instance of <see cref="IDomainEventNotification{TEventType, TKey}"/> according to the specified <paramref name="domainNotificationWithGenericType"/> with a constructor that takes the specified <paramref name="domainEvent"/>.
        /// </summary>
        /// <param name="domainNotificationWithGenericType">Type that must implement <see cref="IDomainEventNotification{TEventType, TKey}"/>.</param>
        /// <param name="domainEvent">Instance of <see cref="IDomainEvent{TKey}"/> to pass to the <paramref name="domainNotificationWithGenericType"/> constructor.</param>
        /// <returns>An instance of <paramref name="domainNotificationWithGenericType"/>, or null if no type is found.</returns>
        IDomainEventNotification<IDomainEvent<TKey>, TKey> GetDomainEventNotification(Type domainNotificationWithGenericType, IDomainEvent<TKey> domainEvent);
    }
}
