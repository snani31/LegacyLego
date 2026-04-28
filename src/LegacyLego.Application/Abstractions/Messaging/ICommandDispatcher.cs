using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface ICommandDispatcher
{
    public Task<Result<TResult>> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<TResult>;

    public Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand;
}