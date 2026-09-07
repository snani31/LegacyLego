using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LegacyLego.IntegrationTests.Infrastructure.Stubs;

public sealed class StubCacheInvalidator : ICacheInvalidator
{
    public Task InvalidateAsync(IEnumerable<object> entities, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}

public static class StubCacheInvalidatorExtensions
{
    public static IServiceCollection AddStubCacheInvalidator(this IServiceCollection services)
    {
        services.RemoveAll<ICacheInvalidator>();
        services.AddScoped<ICacheInvalidator, StubCacheInvalidator>();

        return services;
    }
}