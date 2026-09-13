using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LegacyLego.IntegrationTests.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RemoveHostedServices(this IServiceCollection services)
    {
        var hostedServices = services.Where(d => d.ServiceType == typeof(IHostedService)).ToList();
        foreach (var service in hostedServices)
        {
            services.Remove(service);
        }
        return services;
    }
}