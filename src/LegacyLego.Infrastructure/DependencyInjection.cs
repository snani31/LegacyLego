using Hangfire;
using Hangfire.PostgreSql;
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Infrastructure.BackgroundJobs;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Diagnostics;
using LegacyLego.Infrastructure.Messaging.Abstractions;
using LegacyLego.Infrastructure.Messaging.Bus;
using LegacyLego.Infrastructure.Messaging.Dispatchers;
using LegacyLego.Infrastructure.Messaging.Publishers;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Repositories;
using LegacyLego.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LegacyLego.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.AddOptions<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<OutboxBackgroundWorkerOptions>()
            .BindConfiguration(OutboxBackgroundWorkerOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<PaymentProviderOptions>()
            .BindConfiguration(PaymentProviderOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<HangfireOptions>()
            .BindConfiguration(HangfireOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>()
                              ?? new DatabaseOptions();

        var hangfireOptions = configuration.GetSection(HangfireOptions.SectionName).Get<HangfireOptions>()
                              ?? new HangfireOptions();

        var hangfirePostgreSqlStorageOptions = new PostgreSqlStorageOptions();
        hangfirePostgreSqlStorageOptions.QueuePollInterval = TimeSpan.FromSeconds(hangfireOptions.QueuePollInterval);
        hangfirePostgreSqlStorageOptions.SchemaName = "internal_jobs";

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)

            .AddClasses(classes => classes.AssignableTo(typeof(IIntegrationEventConsumer<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime())
            
            .AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options => 
            {
                options.UseNpgsqlConnection(databaseOptions.ConnectionString);
            }, hangfirePostgreSqlStorageOptions))
            .AddHangfireServer(options =>
            {
                options.WorkerCount = hangfireOptions.WorkerCount;
                options.Queues = new[] { HangfireOptions.CommandHangfireQueueName, "default" };
            })

            .AddDbContext<OrderContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql(dbOptions.ConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(dbOptions.CommandTimeoutSeconds);

                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbOptions.MaxRetryCount,
                        maxRetryDelay: TimeSpan.FromSeconds(dbOptions.MaxRetryDelaySeconds),
                        errorCodesToAdd: null);
                });

                if (dbOptions.EnableSensitiveDataLogging)
                {
                    options.EnableSensitiveDataLogging();
                }

                if (dbOptions.EnableDetailedErrors)
                {
                    options.EnableDetailedErrors();
                }
            })

            .AddScoped<IUnitOfWork, UnitOfWork>()

            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IPaymentRepository, PaymentRepository>()

            .AddSingleton<IIntegrationEventBus, InMemoryIntegrationEventBus>()
            .AddScoped<IIntegrationEventPublisher, LocalIntegrationEventPublisher>()

            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>()

            .AddSingleton<IExceptionMapper, InfrastructureExceptionMapper>()

            .AddSingleton(TimeProvider.System)

            .AddScoped<ICommandBackgroundJobService, HangfireCommandBackgroundJobService>()

            .AddScoped<ICursorSerializer, Base64JsonCursorSerializer>()

            .AddHostedService<OutboxBackgroundWorker>();

        services.AddHttpClient<IPaymentProvider, MockPaymentProvider>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<PaymentProviderOptions>>().Value;

            //базовый адрес для ВСЕХ запросов этого клиента
            client.BaseAddress = new Uri(options.ApiBaseUrl);
        });


        return services;
    }
}