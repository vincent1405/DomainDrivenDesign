using System.Diagnostics.CodeAnalysis;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Infrastructure.Serialization
{
    /// <summary>
    /// Contract to be implemented by a serializer/deserializer of <see cref="IDomainEvent{TKey}"/>.
    /// </summary>
    public interface IEventSerializer
    {
        /// <summary>
        /// Get the serialized form of the specified <paramref name="evt"/>.
        /// </summary>
        /// <typeparam name="TKey">Type of the key identifier the <paramref name="evt"/> refers to.</typeparam>
        /// <param name="evt">Instance of <see cref="IDomainEvent{TKey}"/> to serialize.</param>
        /// <returns>A string containing the serialized <paramref name="evt"/>.</returns>        
        [return: NotNull]
        string Serialize<TKey>([DisallowNull] IDomainEvent<TKey> evt);

        /// <summary>
        /// Get the deserialized <see cref="IDomainEvent{TKey}"/> from the specified <paramref name="payLoad"/>.
        /// </summary>
        /// <typeparam name="TKey">Type of the key identifier the <see cref="IDomainEvent{TKey}"/> refers to.</typeparam>
        /// <param name="payLoad">string containing the serialized <see cref="IDomainEvent{TKey}"/>.</param>
        /// <param name="eventType">Type of the <see cref="IDomainEvent{TKey}"/>.</param>
        /// <returns>An instance of <see cref="IDomainEvent{TKey}"/>.</returns>
        [return: NotNull]
        IDomainEvent<TKey> Deserialize<TKey>([DisallowNull] string payLoad, [DisallowNull] string eventType);
    }
}