using MediatR;

namespace VDew.DomainDrivenDesign.Application.Commands
{
    /// <summary>
    /// Contract to be implemented by a command sent to the system that do not return a result.
    /// </summary>
    public interface ICommand : IRequest
    {
        /// <summary>
        /// Identifier of the command.
        /// </summary>
        Guid CommandId { get; }
    }

    /// <summary>
    /// Contract to be implemented by a command sent to the system and that returns a result.
    /// </summary>
    /// <typeparam name="TResult">Type of the result returned by the command execution.</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        /// <summary>
        /// Identifier of the command.
        /// </summary>
        Guid CommandId { get; }
    }
}
