namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface IEntityInvalidator<in TEntity> where TEntity : class
{
    public Task InvalidateAsync(IEnumerable<TEntity> entities, CancellationToken ct);
}