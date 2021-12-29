using MediatR;

namespace VDew.DomainDrivenDesign.Application.Queries
{
    /// <summary>
    /// Contract to be implemented by a query handler that will process the query and returns the result.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query to handle.</typeparam>
    /// <typeparam name="TResult">Type of the query result.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
