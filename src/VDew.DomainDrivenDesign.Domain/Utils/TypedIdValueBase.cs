using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VDew.DomainDrivenDesign.Domain.Utils
{
    /// <summary>
    /// Base class to represent a typed Id based upon a primitive type.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public abstract class TypedIdValueBase<T> : IEquatable<TypedIdValueBase<T>>
    {
        /// <summary>
        /// Get the value of the <typeparamref name="T"/>.
        /// </summary>
        [NotNull]
        public T Value { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="TypedIdValueBase{T}"/> with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value to set to the <see cref="Value"/> property.</param>
        protected TypedIdValueBase([DisallowNull] T value)
        {
            if (!string.Equals(nameof(value), nameof(Value), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException($"The parameter name '{nameof(value)}' should be the same name the one of the '{nameof(Value)}' property so that it can be correctly deserialized.");
            }
            Value = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override bool Equals([AllowNull] object obj)
        {
            if (obj is TypedIdValueBase<T> other)
            {
                return Equals(other);
            }
            return false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="other"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool Equals([AllowNull] TypedIdValueBase<T> other)
        {
            return other is not null && Value.Equals(other.Value);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        /// <remarks>
        /// For this implementation, the returned value is the hash code of the <see cref="Value"/> property.
        /// </remarks>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>        
        [return: MaybeNull]
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="x"><inheritdoc/></param>
        /// <param name="y"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public static bool operator ==(TypedIdValueBase<T> x, TypedIdValueBase<T> y)
        {
            if (x is null)
            {
                return y is null;
            }

            if (y is null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="x"><inheritdoc/></param>
        /// <param name="y"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public static bool operator !=(TypedIdValueBase<T> x, TypedIdValueBase<T> y)
        {
            return !(x == y);
        }
    }
}
