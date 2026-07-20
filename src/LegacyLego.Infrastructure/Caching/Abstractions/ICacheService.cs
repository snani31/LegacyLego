using LegacyLego.Domain.Shared;

namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface ICacheService
{
    public Task<Result<T>> GetOrCreateAsync<T>(
        string cacheGroup,
        string specificKey,
        Func<Task<Result<T>>> factory,
        TimeSpan ttl,
        CancellationToken ct);
}