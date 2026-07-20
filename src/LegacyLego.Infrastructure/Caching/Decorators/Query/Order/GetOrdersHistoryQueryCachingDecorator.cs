using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace LegacyLego.Infrastructure.Caching.Decorators.Query.Order;

public sealed class GetOrdersHistoryQueryCachingDecorator
    : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    private readonly IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse> _inner;
    private readonly ICacheService _cacheService;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public GetOrdersHistoryQueryCachingDecorator(
        IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse> inner,
        ICacheService cacheService,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _inner = inner;
        _cacheService = cacheService;
        _cacheOptions = cacheOptions;
    }

    public Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var cacheGroup = $"orders:{query.UserId}";

        var safeCursor = string.IsNullOrWhiteSpace(query.Filter.Cursor) ? "first" : query.Filter.Cursor;
        var specificKey = $"cursor:{safeCursor}";

        return _cacheService.GetOrCreateAsync(
            cacheGroup,
            specificKey,
            factory: () => _inner.HandleAsync(query, ct),
            ttl: TimeSpan.FromMinutes(_cacheOptions.CurrentValue.OrdersHistoryMinutesTtl),
            ct);
    }
}