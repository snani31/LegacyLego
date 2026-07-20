using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Caching.Services;

public sealed class RedisCacheService : ICacheService
{
    private readonly ILogger<RedisCacheService> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public RedisCacheService(IConnectionMultiplexer redis,
        ILogger<RedisCacheService> logger,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _redis = redis;
        _logger = logger;
        _cacheOptions = cacheOptions;
    }

    public async Task<Result<T>> GetOrCreateAsync<T>(
        string cacheGroup,
        string specificKey,
        Func<Task<Result<T>>> factory,
        TimeSpan ttl,
        CancellationToken ct)
    {
        var db = _redis.GetDatabase();

        var versionKey = $"{cacheGroup}:version";
        var version = await db.StringGetAsync(versionKey);

        if (!version.HasValue)
        {
            var groupTtl = TimeSpan.FromDays(_cacheOptions.CurrentValue.OrderGroupDaysTtl);
            await db.StringSetAsync(versionKey, "1", groupTtl, When.NotExists);
            version = "1";
        }

        var dataKey = $"{cacheGroup}:v{version}:{specificKey}";

        var cachedData = await db.StringGetAsync(dataKey);
        if (cachedData.HasValue) // Кэш-хит
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize<T>((byte[])cachedData!);
                if (deserialized is not null)
                {
                    _logger.LogDebug("Cache HIT for key: {CacheKey}", dataKey);
                    return Result<T>.Success(deserialized);
                }
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Failed to deserialize cache payload for key: {CacheKey}. Falling back to database.",
                    dataKey);
            }
        }

        // Кэш-мисс
        _logger.LogDebug("Cache MISS for key: {CacheKey}. Fetching data from source.", dataKey);
        var result = await factory();

        if (result.IsSuccess)
        {
            var serialized = JsonSerializer.Serialize(result.Value);
            await db.StringSetAsync(dataKey, serialized, ttl);
        }

        return result;
    }
}