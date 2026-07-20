using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Order = LegacyLego.Domain.Aggregates.Order;

namespace LegacyLego.Infrastructure.Caching.Invalidators;

public sealed class OrderEntityInvalidator : IEntityInvalidator<Order>
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public OrderEntityInvalidator(
        IConnectionMultiplexer redis,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _redis = redis;
        _cacheOptions = cacheOptions;
    }

    public async Task InvalidateAsync(IEnumerable<Order> entities, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        var batch = db.CreateBatch();
        var groupTtl = TimeSpan.FromDays(_cacheOptions.CurrentValue.OrderGroupDaysTtl);

        foreach (var order in entities)
        {
            var userVersionKey = $"orders:{order.ClientId}:version";
            var orderVersionKey = $"order:{order.Id.Value}:version";

            _ = batch.StringIncrementAsync(userVersionKey);
            _ = batch.StringIncrementAsync(orderVersionKey);

            _ = batch.KeyExpireAsync(userVersionKey, groupTtl);
            _ = batch.KeyExpireAsync(orderVersionKey, groupTtl);
        }

        batch.Execute();
        await Task.CompletedTask;
    }
}