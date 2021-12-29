using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDew.DomainDrivenDesign.Domain.Validation
{
    /// <summary>
    /// Abstract implementation of the <see cref="IBusinessRule"/> interface.
    /// <seealso cref="IAsyncBusinessRule"/>
    /// <seealso cref="AsyncBusinessRuleBase"/>
    /// </summary>
    public abstract class BusinessRuleBase : IBusinessRule
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Initialize a new instance of <see cref="BusinessRuleBase"/>.
        /// </summary>
        protected BusinessRuleBase()
        {
            Message = string.Empty;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public abstract bool IsBroken();
    }
}
