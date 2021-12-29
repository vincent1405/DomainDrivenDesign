namespace VDew.DomainDrivenDesign.Domain.Utils
{
    /// <summary>
    /// Implementation of <see cref="TypedIdValueBase{T}"/> based on a <see cref="Guid"/>.
    /// </summary>
    public class GuidValueBase : TypedIdValueBase<Guid>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public GuidValueBase(Guid value) : base(value)
        {
        }
    }
}
