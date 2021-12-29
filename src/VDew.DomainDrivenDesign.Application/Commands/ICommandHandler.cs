using MediatR;

namespace VDew.DomainDrivenDesign.Application.Commands
{
    /// <summary>
    /// Contract to be implemented by a command handler for a command that does not return a result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the <see cref="ICommand"/> associated to this handler.</typeparam>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }

    /// <summary>
    /// Contract to be implemented by a command handler for a command that does return a result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the <see cref="ICommand{TResult}"/> associated to this handler.</typeparam>
    /// <typeparam name="TResult">Type of the command result.</typeparam>
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
