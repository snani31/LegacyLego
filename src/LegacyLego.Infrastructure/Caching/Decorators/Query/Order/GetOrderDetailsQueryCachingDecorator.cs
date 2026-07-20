using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;
namespace LegacyLego.Infrastructure.Caching.Decorators.Query.Order;

public sealed class GetOrderDetailsQueryCachingDecorator
    : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto> _inner;
    private readonly ICacheService _cacheService;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public GetOrderDetailsQueryCachingDecorator(
        IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto> inner,
        ICacheService cacheService,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _inner = inner;
        _cacheService = cacheService;
        _cacheOptions = cacheOptions;
    }

    public Task<Result<OrderDetailsDto>> HandleAsync(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var cacheGroup = $"order:{query.OrderId}";

        var specificKey = "details";

        return _cacheService.GetOrCreateAsync(
            cacheGroup,
            specificKey,
            factory: () => _inner.HandleAsync(query, ct),
            ttl: TimeSpan.FromMinutes(_cacheOptions.CurrentValue.OrderDetailsMinutesTtl),
            ct);
    }
}