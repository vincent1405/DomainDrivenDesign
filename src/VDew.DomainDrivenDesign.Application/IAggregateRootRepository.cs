using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDew.DomainDrivenDesign.Domain;

namespace VDew.DomainDrivenDesign.Application
{
    /// <summary>
    /// Contract to be implemented by a repository to save and load <see cref="IAggregateRoot{TKey}"/> instances.
    /// </summary>
    /// <remarks>
    /// Even if the generic repository pattern is often considered as an anti-pattern (search on Google for 'repository antipattern', this repository is not really generic: it only concerns <see cref="IAggregateRoot{TKey}"/> types.
    /// </remarks>
    /// <typeparam name="TAggregateRoot">Type of the aggregate root that must implement <see cref="IAggregateRoot{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the aggregate root key.</typeparam>
    public interface IAggregateRootRepository<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Asynchronously gets the instance of <typeparamref name="TAggregateRoot"/> from its <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="aggregateRootKey">Key of the aggregate root to retrieve the instance of <typeparamref name="TAggregateRoot"/>.</param>
        /// <param name="cancellationToken">Optional <see cref="CancellationToken"/> to notify for cancellation.</param>
        /// <returns>The instance of <typeparamref name="TAggregateRoot"/> corresponding to the specified <typeparamref name="TKey"/> value.</returns>
        Task<TAggregateRoot?> GetByIdAsync(TKey aggregateRootKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously appends the specified <typeparamref name="TAggregateRoot"/> instance to the repository.
        /// </summary>
        /// <param name="aggregateRoot">Instance of <typeparamref name="TAggregateRoot"/> to save.</param>
        /// <param name="cancellationToken">Optional <see cref="CancellationToken"/> to notify for cancellation.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task AppendAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken);
    }
}
