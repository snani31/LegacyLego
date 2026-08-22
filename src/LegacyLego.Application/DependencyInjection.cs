using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Diagnostics;
using LegacyLego.Application.Options;
using LegacyLego.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyLego.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var options = services.AddOptions<OrderHistoryOptions>()
            .BindConfiguration(OrderHistoryOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)

            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        )

            .AddScoped<IClientProvisioningService, ClientProvisioningService>()

            .AddSingleton<IExceptionMapper, DomainExceptionMapper>();

        return services;
    }
}