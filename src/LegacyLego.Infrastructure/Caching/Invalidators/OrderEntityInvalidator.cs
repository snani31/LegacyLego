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

        var redisTasks = new List<Task>();

        foreach (var order in entities)
        {
            var userVersionKey = $"orders:{order.ClientId}:version";
            var orderVersionKey = $"order:{order.Id.Value}:version";

            redisTasks.Add(batch.StringIncrementAsync(userVersionKey));
            redisTasks.Add(batch.StringIncrementAsync(orderVersionKey));

            redisTasks.Add(batch.KeyExpireAsync(userVersionKey, groupTtl));
            redisTasks.Add(batch.KeyExpireAsync(orderVersionKey, groupTtl));
        }

        batch.Execute();
        await Task.WhenAll(redisTasks); // Фиксируем отправку в Redis до выхода из инвалидатора
    }
}