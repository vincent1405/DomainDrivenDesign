using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using VDew.DomainDrivenDesign.Domain.Events;

namespace VDew.DomainDrivenDesign.Infrastructure.Serialization
{
    /// <summary>
    /// Implementation of the <see cref="IEventSerializer"/> based upon the <see cref="JsonConvert"/> serialization.
    /// </summary>
    internal class EventSerializer : IEventSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateSetterContractResolver()
        };

        private readonly Type[] typesToSerializeDeserialize;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSerializer"/> with an array of <see cref="Type"/> can be serialized/deserialized.
        /// </summary>
        /// <param name="typesToSerializeDeserialize">Array of <see cref="Type"/> that can be serialized/deserialized.</param>
        internal EventSerializer(IEnumerable<Type> typesToSerializeDeserialize)
        {
            if (typesToSerializeDeserialize is null || !typesToSerializeDeserialize.Any())
            {
                throw new ArgumentNullException(nameof(typesToSerializeDeserialize));
            }

            this.typesToSerializeDeserialize = typesToSerializeDeserialize.ToArray();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TKey"><inheritdoc/></typeparam>
        /// <param name="payLoad"><inheritdoc/></param>
        /// <param name="type"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        [return: NotNull]
        public IDomainEvent<TKey> Deserialize<TKey>([DisallowNull] string payLoad, [DisallowNull] string type)
        {
            Type? eventType = GetRegisteredType(type);

            if (eventType == null)
            {
                throw new InvalidOperationException($"Cannot find type '{type}'.");
            }

            object? result = JsonConvert.DeserializeObject(payLoad, eventType, SerializerSettings);
            if (result is null)
            {
                throw new InvalidOperationException($"Cannot deserialize the string '{payLoad}' into an instance of '{type}'.");
            }
            return (IDomainEvent<TKey>)result;
        }

        [return: MaybeNull]
        private Type GetRegisteredType(string type)
        {
            return typesToSerializeDeserialize.FirstOrDefault(t => t.FullName == type) ?? Type.GetType(type, false);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TKey"><inheritdoc/></typeparam>
        /// <param name="evt"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string Serialize<TKey>(IDomainEvent<TKey> evt)
        {
            var json = JsonConvert.SerializeObject((dynamic)evt, SerializerSettings);
            return json;
        }
    }
}
