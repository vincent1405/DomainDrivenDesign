using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Domain.Events
{
    /// <summary>
    /// Contract to be implemented by a domain event, i.e. a domain object that defines an event (something that happens). A domain event is an event that domain experts care about.
    /// </summary>
    /// <typeparam name="TKey">The identity key describes the property of objects that distinguishes them from other objects.</typeparam>
    public interface IDomainEvent<out TKey>
    {
        /// <summary>
        /// Version of the <see cref="IAggregateRoot{TKey}"/> instance on which this domain event happened.
        /// </summary>
        int AggregateRootVersion { get; }

        /// <summary>
        /// Id of the <see cref="IAggregateRoot{TKey}"/> instance on which this domain event happened.
        /// </summary>
        [NotNull]
        TKey AggregateRootId { get; }

        /// <summary>
        /// UTC timestamp when this domain event happened.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
