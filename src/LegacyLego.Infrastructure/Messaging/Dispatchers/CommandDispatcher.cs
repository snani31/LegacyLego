using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Dispatchers;

public sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        var commandType = command.GetType();

        var wrapper = WrapperCache.GetOrAdd(commandType, type =>
        {
            var concreteWrapperType = typeof(CommandWrapperR<,>).MakeGenericType(type, typeof(TResult));
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((CommandWrapperR<TResult>)wrapper).HandleAsync(command, serviceProvider, ct);
    }

    public Task<Result> DispatchAsync(ICommand command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        var commandType = command.GetType();

        var wrapper = WrapperCache.GetOrAdd(commandType, type =>
        {
            var concreteWrapperType = typeof(CommandWrapper<>).MakeGenericType(type);
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((CommandWrapper)wrapper).HandleAsync(command, serviceProvider, ct);
    }
}

file abstract class CommandWrapperR<TResult>
{
    public abstract Task<Result<TResult>> HandleAsync(ICommand<TResult> command, IServiceProvider provider, CancellationToken ct);
}

file sealed class CommandWrapperR<TCommand, TResult> : CommandWrapperR<TResult>
    where TCommand : ICommand<TResult>
{
    public override Task<Result<TResult>> HandleAsync(ICommand<TResult> command, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync((TCommand)command, ct);
    }
}

file abstract class CommandWrapper
{
    public abstract Task<Result> HandleAsync(ICommand command, IServiceProvider provider, CancellationToken ct);
}

file sealed class CommandWrapper<TCommand> : CommandWrapper
    where TCommand : ICommand
{
    public override Task<Result> HandleAsync(ICommand command, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();
        return handler.HandleAsync((TCommand)command, ct);
    }
}