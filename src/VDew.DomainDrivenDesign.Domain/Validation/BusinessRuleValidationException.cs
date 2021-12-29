using System.Runtime.Serialization;

namespace VDew.DomainDrivenDesign.Domain.Validation
{
    /// <summary>
    /// <see cref="Exception"/> to be thrown when a business rule is broken.
    /// </summary>
    [Serializable]
    public class BusinessRuleValidationException : Exception
    {
        /// <summary>
        /// Utility method to create a <see cref="BusinessRuleValidationException"/> from a <see cref="IBusinessRule"/> instance.
        /// </summary>
        /// <param name="businessRule">Instance of <see cref="IBusinessRule"/> to use to generate the <see cref="BusinessRuleValidationException"/>.</param>
        /// <returns>A new <see cref="BusinessRuleValidationException"/> instance whose message is set according to the <see cref="IBusinessRule.Message"/> property.</returns>
        public static BusinessRuleValidationException CreateBusinessRuleValidationException(IBusinessRule businessRule)
        {
            if (businessRule is null)
            {
                throw new ArgumentNullException(nameof(businessRule));
            }

            return new BusinessRuleValidationException(businessRule.Message);
        }

        /// <summary>
        /// Utility method to create a <see cref="BusinessRuleValidationException"/> from a <see cref="IBusinessRule"/> instance.
        /// </summary>
        /// <param name="businessRule">Instance of <see cref="IAsyncBusinessRule"/> to use to generate the <see cref="BusinessRuleValidationException"/>.</param>
        /// <returns>A new <see cref="BusinessRuleValidationException"/> instance whose message is set according to the <see cref="IAsyncBusinessRule.Message"/> property.</returns>
        public static BusinessRuleValidationException CreateBusinessRuleValidationException(IAsyncBusinessRule businessRule)
        {
            if (businessRule is null)
            {
                throw new ArgumentNullException(nameof(businessRule));
            }

            return new BusinessRuleValidationException(businessRule.Message);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BusinessRuleValidationException()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"><inheritdoc/></param>
        public BusinessRuleValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"><inheritdoc/></param>
        /// <param name="innerException"><inheritdoc/></param>
        public BusinessRuleValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="info"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
