using VDew.DomainDrivenDesign.Infrastructure.EventsStorage;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsDispatching
{
    public interface IOutboxMessageStore
    {
        Task RegisterEventToNotifyAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken);
    }
}