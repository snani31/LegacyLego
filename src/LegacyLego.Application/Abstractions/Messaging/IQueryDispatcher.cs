using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IQueryDispatcher
{
    public Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default);
}