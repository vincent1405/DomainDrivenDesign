using MediatR;
using System.Diagnostics.CodeAnalysis;
using VDew.DomainDrivenDesign.Application.DomainEvents;
using VDew.DomainDrivenDesign.Domain;
using VDew.DomainDrivenDesign.Domain.Events;
using VDew.DomainDrivenDesign.Infrastructure.EventsStorage;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsDispatching
{
    /// <summary>
    /// Implementation of the <see cref="IDomainEventsDispatcher{TAggregateRoot, TKey}"/> contract using an <see cref="IEventStore"/>, an <see cref="IDomainEventResolver{TKey}"/> to resolve types and an <see cref="IMediator"/> to publish events.
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of the <see cref="IAggregateRoot{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the <see cref="IAggregateRoot{TKey}"/> key.</typeparam>
    public class DomainEventsDispatcher<TAggregateRoot, TKey> : IDomainEventsDispatcher<TAggregateRoot, TKey> where TAggregateRoot : IAggregateRoot<TKey>
    {
        private readonly IMediator mediator;
        private readonly IDomainEventResolver<TKey> domainEventResolver;
        private readonly IOutboxMessageStore outboxMessageStore;

        /// <summary>
        /// Initialize a new instance of <see cref="DomainEventsDispatcher{TAggregateRoot, TKey}"/> with the specified <see cref="IEventStore"/>, <see cref="IDomainEventResolver{TKey}"/> and <see cref="IMediator"/> instances.
        /// </summary>
        /// <param name="outboxMessageStore">Optional instance of <see cref="IOutboxMessageStore"/> if domain events must be published later.</param>
        /// <param name="domainEventResolver">Instance of <see cref="IDomainEventResolver{TKey}"/> to resolve subscribers to domain events.</param>
        /// <param name="mediator">Instance of <see cref="IMediator"/> to publish domain events.</param>
        public DomainEventsDispatcher(IOutboxMessageStore outboxMessageStore, IDomainEventResolver<TKey> domainEventResolver, IMediator mediator)
        {
            this.outboxMessageStore = outboxMessageStore;
            this.domainEventResolver = domainEventResolver ?? throw new ArgumentNullException(nameof(domainEventResolver));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRoot"><inheritdoc/></param>
        /// <param name="cancellationToken"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task DispatchDomainEventsAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<IDomainEvent<TKey>> domainEvents = aggregateRoot.Events;

            List<IDomainEventWrapper<IDomainEvent<TKey>, TKey>> domainEventsWrapper = (from domainEvent in domainEvents
                                                                                       let domainEventWrapperType = typeof(IDomainEventWrapper<,>)
                                                                                       let domainEventWrapperWithGenericType = domainEventWrapperType.MakeGenericType(new[] { domainEvent.GetType(), typeof(TKey) })
                                                                                       let domainEventToRise = domainEventResolver.GetDomainEventWrapper(domainEventWrapperWithGenericType, domainEvent)
                                                                                       where domainEventToRise != null
                                                                                       select domainEventToRise).ToList();

            List<IDomainEventNotification<IDomainEvent<TKey>, TKey>> domainEventsNotification = (from domainEvent in domainEvents
                                                                                                 let domainEventType = typeof(IDomainEventNotification<,>) // Get the genereric type IDomainEventNotification<TEventType>
                                                                                                 let domainNotificationWithGenericType = domainEventType.MakeGenericType(new[] { domainEvent.GetType(), typeof(TKey) }) // Create the generic type to match the type of the domain event
                                                                                                 let domainNotification = domainEventResolver.GetDomainEventNotification(domainNotificationWithGenericType, domainEvent) // Get the real implementation of domainNotificationWithGenericType from the IoC, whose constructor has a parameter named domainEvent, and set the value of this parameter to the original domainEvent
                                                                                                 where domainNotification != null // Check it is not null 
                                                                                                 select domainNotification).ToList();

            aggregateRoot.ClearEvents();

            IEnumerable<Task> publishDomainEventTasks = domainEventsWrapper.Select(async (de) =>
            {
                await mediator.Publish(de, cancellationToken);
            });

            await Task.WhenAll(publishDomainEventTasks);

            foreach (IDomainEventNotification<IDomainEvent<TKey>, TKey> domainEventNotification in domainEventsNotification)
            {
                string type = GetDomainEventNotificationType(domainEventNotification);
                var data = System.Text.Json.JsonSerializer.Serialize(domainEventNotification);
                var outboxMessage = new OutboxMessage(
                    occurredOn: domainEventNotification.DomainEvent.OccurredOn,
                    type: type,
                    data: data
                );

                if (outboxMessageStore != null)
                {
                    await outboxMessageStore.RegisterEventToNotifyAsync(outboxMessage, cancellationToken);
                }
            }
        }

        [return: NotNull]
        private string GetDomainEventNotificationType([DisallowNull] IDomainEventNotification<IDomainEvent<TKey>, TKey> domainEventNotification)
        {
            Type type = domainEventNotification.GetType();
            if (type == null)
            {
                throw new InvalidOperationException($"The {nameof(GetType)} method invocation of the current instance of {nameof(DomainNotificationBase<IDomainEvent<TKey>, TKey>)} returns null.");
            }
            return type.FullName ?? throw new InvalidOperationException($"The {nameof(Type.FullName)} property of the current instance of {nameof(DomainNotificationBase<IDomainEvent<TKey>, TKey>)} returns null.");
        }
    }
}
