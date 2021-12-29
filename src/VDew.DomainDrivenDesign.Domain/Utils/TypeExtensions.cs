namespace VDew.DomainDrivenDesign.Domain.Utils
{
    /// <summary>
    /// Class that contains extension methods to <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the underlying type of a <see cref="Nullable"/> type.
        /// </summary>
        /// <param name="type">Type to get underlying type.</param>
        /// <returns>The underlying type if type is <see cref="Nullable{T}"/>, else <paramref name="type"/>.</returns>
        public static Type UnwrapNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// Get a value indicating if the <paramref name="extendType"/> is inherited from <paramref name="baseType"/> (true) or not (false).
        /// </summary>
        /// <param name="extendType">Child type.</param>
        /// <param name="baseType">Base type.</param>
        /// <returns>True if <paramref name="extendType"/> inherits from <paramref name="baseType"/>, else false.</returns>
        public static bool IsAssignableFromWithGeneric(this Type extendType, Type baseType)
        {
            if (baseType is null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            while (extendType is not null && !baseType.IsAssignableFrom(extendType))
            {
                if (extendType.Equals(typeof(object)))
                {
                    return false;
                }
                if (extendType.IsGenericType && !extendType.IsGenericTypeDefinition)
                {
                    extendType = extendType.GetGenericTypeDefinition();
                }
                else if(extendType is not null && extendType.BaseType is not null)
                {
                    extendType = extendType.BaseType;
                }
            }
            return true;
        }
    }
}
