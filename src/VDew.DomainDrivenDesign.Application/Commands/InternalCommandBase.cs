﻿namespace VDew.DomainDrivenDesign.Application.Commands
{
    /// <summary>
    /// Base class to implement an internal command, i.e. a command the system sends to itself.
    /// This type of command is thought to be persisted to database and executed later.
    /// </summary>
    /// <typeparam name="TResult">Type of the command result, if any. Else, use <see cref="MediatR.Unit"/>.</typeparam>
    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Guid CommandId { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="InternalCommandBase{TResult}"/> with an autogenerated value for <see cref="CommandId"/>.
        /// </summary>
        protected InternalCommandBase() => CommandId = Guid.NewGuid();

        /// <summary>
        /// Initialize a new instance of <see cref="InternalCommandBase{TResult}"/> with the specified value for <see cref="CommandId"/>.
        /// </summary>
        /// <param name="commandId">Identifier of the command.</param>
        protected InternalCommandBase(Guid commandId) => CommandId = commandId;
    }
}
