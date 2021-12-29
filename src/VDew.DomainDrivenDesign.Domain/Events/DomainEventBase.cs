using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VDew.DomainDrivenDesign.Domain.Events
{
    /// <summary>
    /// Base class for implementation of the <see cref="IDomainEvent{TKey}"/> interface.
    /// </summary>
    /// <typeparam name="TAggregateRoot"><inheritdoc></inheritdoc>/></typeparam>
    /// <typeparam name="TKey"><inheritdoc/></typeparam>
    /// <remarks>
    /// You have to define Properties with public getter and PRIVATE setter (not readonly properties) else the deserializer will not be able to set the properties values.
    /// </remarks>
    public abstract class DomainEventBase<TAggregateRoot, TKey> : IDomainEvent<TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int AggregateRootVersion { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [NotNull]
        public TKey AggregateRootId { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime OccurredOn { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        [JsonConstructor]
        protected DomainEventBase()
        {
            AggregateRootId = default!;
            OccurredOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="DomainEventBase{TAggregateRoot, TKey}"/>.
        /// </summary>
        /// <param name="aggregateRoot">Instance of <typeparamref name="TAggregateRoot"/> the event happened on.</param>
        protected DomainEventBase(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            AggregateRootVersion = aggregateRoot.Version;
            AggregateRootId = aggregateRoot.Id;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
