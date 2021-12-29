using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsStorage
{
    /// <summary>
    /// Serialized representation of a <see cref="IDomainEvent{TKey}"/>.
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// Get or set the unique identifier of the serialized event.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Get or set the <see cref="Type.FullName"/> of the serialized domain event.
        /// </summary>
        [NotNull]
        public string EventType { get; set; }

        /// <summary>
        /// Get or set the string representation of the <see cref="Domain.EntityBase{TKey}.Id"/> unique identifier.
        /// </summary>
        [NotNull]
        public string AggregateId { get; set; }

        /// <summary>
        /// Get or set the current version <see cref="Domain.IAggregateRoot{TKey}.Version"/>.
        /// </summary>        
        public int AggregateVersion { get; set; }

        /// <summary>
        /// Get or set the date and time the event occurred on.
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Get or set the schema version of the <see cref="EventData.PayLoad"/>.
        /// </summary>
        public int PayLoadSchemaVersion { get; set; }

        /// <summary>
        /// Get or set the payload of the event, i.e. the serialized <see cref="IDomainEvent{TKey}"/>.
        /// </summary>
        [NotNull]
        public string PayLoad { get; set; }

        public EventData()
        {
            PayLoad = string.Empty;
            EventType = string.Empty;
            AggregateId = string.Empty;
        }
    }
}