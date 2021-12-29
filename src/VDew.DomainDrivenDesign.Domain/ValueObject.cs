using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Domain
{
    /// <summary>
    /// Base class for all value objects.
    /// <br/>
    /// An object that contains attributes but has no conceptual identity. They should be treated as immutable.<br/>
    /// Example: When people exchange business cards, they generally do not distinguish between each unique card; they are only concerned about the information printed on the card.In this context, business cards are value objects.
    /// </summary>
    /// <typeparam name="T">Type of the ValueObject.</typeparam>
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override bool Equals([AllowNull] object obj)
        {
            if (obj is T other)
            {
                return EqualsCore(other);
            }

            return false;
        }

        /// <summary>
        /// Method to test instances of <see cref="ValueObject{T}"/> equality.
        /// </summary>
        /// <param name="other">Other instance of <see cref="ValueObject{T}"/> to test for equality.</param>
        /// <returns>True if the current instance can be considered as equal to <paramref name="other"/>, else false.</returns>
        protected abstract bool EqualsCore([AllowNull] T other);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// Calculates the hash code for the current instance of <see cref="ValueObject{T}"/>.
        /// </summary>
        /// <returns>A hash code that shall be unique for the current instance.</returns>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="a"><inheritdoc/></param>
        /// <param name="b"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="a"><inheritdoc/></param>
        /// <param name="b"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
