using MediatR;

namespace VDew.DomainDrivenDesign.Application.Queries
{
    /// <summary>
    /// Contract to be implemented by any query, i.e. a request to the system that does not change its internal state, but only return data.
    /// </summary>
    /// <typeparam name="TResult">Type of the result of the query.</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
