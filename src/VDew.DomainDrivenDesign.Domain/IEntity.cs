using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace VDew.DomainDrivenDesign.Domain
{
    /// <summary>
    /// Contract to be implemented to represent a DDD entity.
    /// An entity is an object that is not defined by its attributes, but rather by a thread of continuity and its identity.
    /// </summary>
    /// <typeparam name="TKey">Type of the identity for the entity, which enables to distinguish between two <see cref="IEntity{TKey}"/> instances.</typeparam>
    public interface IEntity<out TKey>
    {
        /// <summary>
        /// Get the identity of the current instance.
        /// </summary>
        [NotNull]
        TKey Id { get; }
    }
}
