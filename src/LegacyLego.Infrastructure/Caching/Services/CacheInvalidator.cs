using LegacyLego.Infrastructure.Caching.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyLego.Infrastructure.Caching.Services;

public sealed class CacheInvalidator : ICacheInvalidator
{
    private readonly IServiceProvider _serviceProvider;

    public CacheInvalidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InvalidateAsync(IEnumerable<object> entities, CancellationToken ct)
    {
        var groupedEntities = entities.GroupBy(e => e.GetType());

        foreach (var group in groupedEntities)
        {
            var entityType = group.Key;

            var invalidatorType = typeof(IEntityInvalidator<>).MakeGenericType(entityType);

            var invalidator = _serviceProvider.GetService(invalidatorType);

            if (invalidator is null)
                continue;

            var method = invalidatorType.GetMethod(nameof(IEntityInvalidator<object>.InvalidateAsync));

            if (method is not null)
            {
                var typedList = CastList(group, entityType);

                await (Task)method.Invoke(invalidator, [typedList, ct])!;
            }
        }
    }

    private static object CastList(IEnumerable<object> source, Type targetType)
    {
        var castMethod = typeof(Enumerable)
            .GetMethod(nameof(Enumerable.Cast))!
            .MakeGenericMethod(targetType);

        var toListMethod = typeof(Enumerable)
            .GetMethod(nameof(Enumerable.ToList))!
            .MakeGenericMethod(targetType);

        var casted = castMethod.Invoke(null, [source]);
        return toListMethod.Invoke(null, [casted])!;
    }
}