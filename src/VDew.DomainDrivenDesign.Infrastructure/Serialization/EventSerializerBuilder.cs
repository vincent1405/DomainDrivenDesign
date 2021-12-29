using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VDew.DomainDrivenDesign.Domain.Utils;

namespace VDew.DomainDrivenDesign.Infrastructure.Serialization
{
    /// <summary>
    /// Class to generate a instance of <see cref="IEventSerializer"/>.
    /// </summary>
    public class EventSerializerBuilder
    {
        private readonly Assembly[] assembliesToScan;

        private readonly Type baseTypeForTypeToSerialzeDeserialize;

        /// <summary>
        /// Initialize a new instance of <see cref="EventSerializerBuilder"/> that will build an <see cref="IEventSerializer"/> by scaning the specified <paramref name="assembliesToScan"/>.
        /// If the <paramref name="baseTypeForTypeToSerialzeDeserialize"/> is specified, only types that derive from this base type will be serialized/deserialized.
        /// </summary>
        /// <param name="baseTypeForTypeToSerialzeDeserialize">Optional base type for types that will be serialized/deserialized.</param>
        /// <param name="assembliesToScan">Array of <see cref="Assembly"/> that will be searched for </param>
        public EventSerializerBuilder(Type baseTypeForTypeToSerialzeDeserialize, params Assembly[] assembliesToScan)
        {
            this.baseTypeForTypeToSerialzeDeserialize = baseTypeForTypeToSerialzeDeserialize;
            this.assembliesToScan = assembliesToScan == null || assembliesToScan.Length == 0 ? throw new System.ArgumentNullException(nameof(assembliesToScan)) : assembliesToScan;
        }

        /// <summary>
        /// Builds the implementation of <see cref="IEventSerializer"/>. 
        /// If <paramref name="allowReadOnlyProperties"/> is set to false, the builder will check that all not inherited properties are readable/writable (no readonly properties).
        /// If types with readonly properties are detected, an <see cref="InvalidOperationException"/> will be thrown, the message will give you the involved types and their readonly properties.
        /// </summary>
        /// <param name="allowReadOnlyProperties">Value indicating if the builder must search for readonly properties (false) or ignore them (true).</param>
        /// <returns>An implementation of <see cref="IEventSerializer"/>.</returns>
        public IEventSerializer Build(bool allowReadOnlyProperties = false)
        {
            List<Type> typesThatInheritFromT = assembliesToScan.SelectMany(a => a.GetTypes()).ToList();

            if (baseTypeForTypeToSerialzeDeserialize != null)
            {
                typesThatInheritFromT = typesThatInheritFromT.Where(t => t.IsAssignableFromWithGeneric(baseTypeForTypeToSerialzeDeserialize)).ToList();
            }

            if (!allowReadOnlyProperties)
            {
                var typesWithReadonlyProperties = typesThatInheritFromT.Where(t => t.GetProperties().Any(p => !p.CanWrite && p.DeclaringType == t));
                if (typesWithReadonlyProperties.Any())
                {
                    StringBuilder errorMessage = new();

                    foreach (var eventType in typesWithReadonlyProperties)
                    {
                        errorMessage.Append($"The type {eventType.FullName} declares readonly properties, which will prevent the event deserializer to properly set them.{Environment.NewLine}Properties:{Environment.NewLine}");
                        foreach (PropertyInfo pi in eventType.GetProperties().Where(p => !p.CanWrite && p.DeclaringType == eventType))
                        {
                            errorMessage.AppendLine($"- {pi.Name}");
                        }
                    }
                    throw new InvalidOperationException(errorMessage.ToString());
                }
            }
            return new EventSerializer(typesThatInheritFromT);
        }
    }
}
