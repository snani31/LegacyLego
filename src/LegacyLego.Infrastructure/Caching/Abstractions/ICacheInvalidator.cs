namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface ICacheInvalidator
{
    public Task InvalidateAsync(IEnumerable<object> entities, CancellationToken ct);
}