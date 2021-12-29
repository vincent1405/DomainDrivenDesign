using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using VDew.DomainDrivenDesign.Domain.Events;
using VDew.DomainDrivenDesign.Domain.Validation;

namespace VDew.DomainDrivenDesign.Domain
{
    /// <summary>
    /// Base class for implementation of <see cref="IAggregateRoot{TKey}"/>.
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of the actual Aggregate Root.</typeparam>
    /// <typeparam name="TKey"><inheritdoc/></typeparam>
    /// <example>
    /// <code>
    /// public class ActualAggregateRoot : AggregateRootBase{ActualAggregateRoot, Guid}
    /// </code>
    /// </example>
    public abstract class AggregateRootBase<TAggregateRoot, TKey> : EntityBase<TKey>, IAggregateRoot<TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private static readonly ConstructorInfo Ctor;

        /// <summary>
        /// Static constructor to initialize the <see cref="Ctor"/> field.
        /// It is done in the static constructor (an not in the <see cref="Create(IEnumerable{IDomainEvent{TKey}})"/> method)  in order to throw a <see cref="InvalidOperationException"/> when the program starts.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <typeparamref name="TAggregateRoot"/> does not define a parameterless constructor.</exception>
        static AggregateRootBase()
        {
            var aggregateType = typeof(TAggregateRoot);
            Ctor = aggregateType.GetConstructor(bindingAttr: BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: Array.Empty<Type>(),
                modifiers: Array.Empty<ParameterModifier>())!;

            if (Ctor == null)
            {
                throw new InvalidOperationException($"The type '{aggregateType.FullName}' does not provide a parameterless constructor.");
            }
        }

        /// <summary>
        /// Asynchronously checks the specified <see cref="IAsyncBusinessRule"/>. Throws a <see cref="BusinessRuleValidationException"/> if the specified <paramref name="businessRule"/> is broken.
        /// </summary>
        /// <param name="businessRule"><see cref="IAsyncBusinessRule"/> to check.</param>
        /// <param name="cancellationToken">Token to indicate the current task must be canceled.</param>
        /// <exception cref="BusinessRuleValidationException">Thrown when the <see cref="IAsyncBusinessRule"/> is broken.</exception>
        protected static async Task CheckBusinessRuleAsync(IAsyncBusinessRule businessRule, CancellationToken cancellationToken)
        {
            if (await businessRule.IsBrokenAsync(cancellationToken))
            {
                throw BusinessRuleValidationException.CreateBusinessRuleValidationException(businessRule);
            }
        }

        /// <summary>
        /// Synchronously checks the specified <see cref="IBusinessRule"/>. Throws a <see cref="BusinessRuleValidationException"/> if the specified <paramref name="businessRule"/> is broken.
        /// </summary>
        /// <param name="businessRule"><see cref="IBusinessRule"/> to check.</param>
        /// <exception cref="BusinessRuleValidationException">Thrown when the <see cref="IBusinessRule"/> is broken.</exception>
        protected static void CheckBusinessRule(IBusinessRule businessRule)
        {
            if (businessRule.IsBroken())
            {
                throw BusinessRuleValidationException.CreateBusinessRuleValidationException(businessRule);
            }
        }

        /// <summary>
        /// Initialize a new instance of the <typeparamref name="TAggregateRoot"/> by playing all <paramref name="domainEvents"/> on it.
        /// </summary>
        /// <param name="domainEvents"><see cref="IEnumerable{T}"/> containing all the <see cref="IDomainEvent{TKey}"/> to play on the created instance.</param>
        /// <remarks>
        /// In order to create a new instance of <typeparamref name="TAggregateRoot"/>, the type must contain a parameterless constructor which will be invoked.
        /// </remarks>
        /// <returns>A new instance of <typeparamref name="TAggregateRoot"/>.</returns>
        public static TAggregateRoot Create([DisallowNull] IEnumerable<IDomainEvent<TKey>> domainEvents)
        {
            if (domainEvents == null || !domainEvents.Any())
            {
                throw new ArgumentNullException(nameof(domainEvents));
            }

            var instance = (TAggregateRoot)Ctor.Invoke(Array.Empty<object>());

            if (instance is AggregateRootBase<TAggregateRoot, TKey> baseInstance)
            {
                foreach (var evt in domainEvents)
                {
                    baseInstance.Id = evt.AggregateRootId;
                    baseInstance.AddEvent(evt);
                }
            }

            instance.ClearEvents();
            return instance;
        }

        private readonly ConcurrentQueue<IDomainEvent<TKey>> domainEvents = new();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IReadOnlyCollection<IDomainEvent<TKey>> Events => domainEvents.ToImmutableArray();

        /// <summary>
        /// Initialize a new instance of <see cref="AggregateRootBase{TAggregateRoot, TKey}"/>.
        /// </summary>
        protected AggregateRootBase() { }

        /// <summary>
        /// Initialize a new instance of <see cref="AggregateRootBase{TAggregateRoot, TKey}"/> with the specified identifier.
        /// </summary>
        /// <param name="id">Value of the identifier.</param>
        protected AggregateRootBase([DisallowNull] TKey id) : base(id) { }

        /// <summary>
        /// Method to call to append a new <see cref="IDomainEvent{TKey}"/> to the current aggregate root.
        /// </summary>
        /// <param name="domainEvent"><see cref="IDomainEvent{TKey}"/> to apply to the current <see cref="AggregateRootBase{TAggregateRoot, TKey}"/>.</param>
        /// <remarks>
        /// This method will call the <see cref="Apply(IDomainEvent{TKey})"/> method with the specified <paramref name="domainEvent"/>.
        /// </remarks>
        protected void AddEvent(IDomainEvent<TKey> domainEvent)
        {
            domainEvents.Enqueue(domainEvent);

            Apply(domainEvent);

            Version++;
        }

        /// <summary>
        /// Method that will be called when a <see cref="IDomainEvent{TKey}"/> is added to the current <see cref="AggregateRootBase{TAggregateRoot, TKey}"/>.
        /// </summary>
        /// <remarks>
        /// This method can be implemented using the so-called <i>Switch statements with patterns</i>:
        /// <code>
        /// switch(domainEvent)
        /// {
        ///     case AggregateRootCreatedEvent aggregateRootCreated:
        ///         this.Id = aggregateRootCreated.Id;
        ///         this.Name = aggregateRootCreated.Name;
        ///         break;
        ///         
        ///     case AggregateRootRenamedEvent aggregateRootRenamed:
        ///         this.Name = aggregateRootRenamed.Name;
        ///         break;
        ///         
        ///     default:
        ///         throw new NotImplementedException($"Domain events of type '{domainEvent.GetType()}' are not processed in '{GetType()}.nameof(Apply))' method.");
        /// }
        /// </code>
        /// </remarks>
        /// <param name="domainEvent"></param>
        protected abstract void Apply(IDomainEvent<TKey> domainEvent);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void ClearEvents()
        {
            domainEvents.Clear();
        }
    }
}
