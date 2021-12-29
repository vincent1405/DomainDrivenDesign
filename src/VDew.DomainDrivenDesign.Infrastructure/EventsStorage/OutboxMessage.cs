using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Infrastructure.EventsStorage
{
    /// <summary>
    /// Class to store an outbox message, i.e. a message that must be sent to notify other services that a <see cref="IDomainEvent{TKey}"/> happened.
    /// </summary>
    public class OutboxMessage
    {
        /// <summary>
        /// Get or set the unique identifier of the current <see cref="OutboxMessage"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the date and time the event occurred on.
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Get or set the <see cref="Type.FullName"/> of the event type.
        /// </summary>
        [MaybeNull]
        public string Type { get; set; }

        /// <summary>
        /// Get or set the serialized event.
        /// </summary>
        [MaybeNull]
        public string Data { get; set; }

        /// <summary>
        /// Get or set the date and time the outbox message was processed on.
        /// </summary>
        /// <remarks>This value must be set once the event was successfully processed.</remarks>
        public DateTime? ProcessedOn { get; set; }

        private OutboxMessage()
        {

        }

        /// <summary>
        /// Initialize a new instance of <see cref="OutboxMessage"/> with the specified values.
        /// </summary>
        /// <param name="occurredOn">Date and time the embedded domain event occurred on.</param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public OutboxMessage(DateTime occurredOn, [DisallowNull] string type, [DisallowNull] string data)
        {
            Id = Guid.NewGuid();
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }
    }
}
