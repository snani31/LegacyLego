using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging;

public sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        var queryType = query.GetType();

        var wrapper = WrapperCache.GetOrAdd(queryType, type =>
        {
            var concreteWrapperType = typeof(QueryWrapper<,>).MakeGenericType(type, typeof(TResult));
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((QueryWrapper<TResult>)wrapper).HandleAsync(query, serviceProvider, ct);
    }
}

file abstract class QueryWrapper<TResult>
{
    public abstract Task<Result<TResult>> HandleAsync(IQuery<TResult> query, IServiceProvider provider, CancellationToken ct);
}

file sealed class QueryWrapper<TQuery, TResult> : QueryWrapper<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<Result<TResult>> HandleAsync(IQuery<TResult> query, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync((TQuery)query, ct);
    }
}