using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    public Task<Result> HandleAsync(TCommand command,CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}