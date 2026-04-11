using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Query;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> Handle(TQuery query,CancellationToken cancellationToken);
}