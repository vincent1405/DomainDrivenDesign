using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Domain
{
    /// <summary>
    /// Base class for implementation of <see cref="IEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey"><inheritdoc/></typeparam>
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [NotNull]
        public TKey Id { get; protected set; }

        /// <summary>
        /// Initialize a new instance of <see cref="EntityBase{TKey}"/>.
        /// </summary>
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        protected EntityBase()
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {
        }

        /// <summary>
        /// Iniitialize a new instance of <see cref="EntityBase{TKey}"/> with the specified identifier.
        /// </summary>
        /// <param name="id">Identifier of the <see cref="EntityBase{TKey}"/>.</param>
        protected EntityBase([DisallowNull] TKey id)
        {
            Id = id;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool Equals([AllowNull]object obj)
        {
            if(obj is EntityBase<TKey> other)
            {
                return GetType() == other.GetType() && EqualityComparer<TKey>.Default.Equals(Id, other.Id);
            }
            return false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(GetType(), Id);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static bool operator ==(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            if (entity1 is null)
            {
                return entity2 is null;
            }

            if (entity2 is null) return false;
            return EqualityComparer<TKey>.Default.Equals(entity1.Id, entity2.Id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static bool operator !=(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
