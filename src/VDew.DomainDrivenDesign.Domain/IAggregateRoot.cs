using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Domain
{
    /// <summary>
    /// Contract to be implemented by any collection of objects that are bound together by a root entity.
    /// It guarantees the the consistency of changes being made within the aggregate by forbidding external objects from holding references to its members.
    /// </summary>
    /// <typeparam name="TKey">Type of the identity for the aggregate root, which enables to distinguish between two <see cref="IAggregateRoot{TKey}"/> instances.</typeparam>
    public interface IAggregateRoot<out TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Get the current version of the <see cref="IAggregateRoot{TKey}"/> instance.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Get a readonly collection of <see cref="IDomainEvent{TKey}"/> that happened to the current <see cref="IAggregateRoot{TKey}"/> instance but have not been persisted to the repository yet.
        /// </summary>
        IReadOnlyCollection<IDomainEvent<TKey>> Events { get; }

        /// <summary>
        /// Clear the <see cref="Events"/> collection (logically once events have been persisted).
        /// </summary>
        void ClearEvents();
    }
}
