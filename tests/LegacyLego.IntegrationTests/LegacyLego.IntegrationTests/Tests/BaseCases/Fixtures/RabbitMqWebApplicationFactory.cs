using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Infrastructure.BackgroundJobs;
using LegacyLego.Infrastructure.Options;
using LegacyLego.IntegrationTests.Infrastructure.Fakes;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NSubstitute;
using RabbitMQ.Client;
using Testcontainers.RabbitMq;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;

public class KeycloakRegistrationWebApplicationFactory : DefaultWebApplicationFactory
{
    private const string RABBIT_USER = "Rabbit";
    private const string RABBIT_PASSWORD = "RabbitPwd";

    public FakeIdentityProviderService IdentityProviderFake { get; } = new();

    public RabbitMqOptions RabbitMqOptions { get; private set; } = null!;

    private static readonly string DefinitionsPath = Path.Combine(
        AppContext.BaseDirectory, "infrastructure", "rabbitmq", "definitions.json");

    private static readonly string ConfigPath = Path.Combine(
        AppContext.BaseDirectory, "infrastructure", "rabbitmq", "rabbitmq.conf");

    public readonly RabbitMqContainer RabbitContainer = new RabbitMqBuilder("rabbitmq:3-management-alpine")
    .WithUsername(RABBIT_USER)
    .WithPassword(RABBIT_PASSWORD)
    .WithBindMount(ConfigPath, "/etc/rabbitmq/rabbitmq.conf", AccessMode.ReadOnly)
    .WithBindMount(DefinitionsPath, "/etc/rabbitmq/definitions.json", AccessMode.ReadOnly)
    .Build();

    public override async Task InitializeAsync()
    {
        await RabbitContainer.StartAsync();

        await base.InitializeAsync();

        RabbitMqOptions = Services.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        // Запуск шины MassTransit для всей группы тестов
        var busControl = Services.GetRequiredService<IBusControl>();
        await busControl.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Базовый вызов сделает RemoveHostedServices() и настроит Postgres + StubCache
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            // Переопределить для RabbitMqOptions только порт и хост из Testcontainers
            services.PostConfigure<RabbitMqOptions>(options =>
            {
                options.Host = RabbitContainer.Hostname;
                options.Port = RabbitContainer.GetMappedPublicPort(5672);
                options.Username = RABBIT_USER;
                options.Password = RABBIT_PASSWORD;
            });

            services.RemoveAll<IConnectionFactory>();
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                return new ConnectionFactory
                {
                    HostName = options.Host,
                    Port = options.Port,
                    UserName = options.Username,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost
                };
            });

            // Используем фейк вместо настоящего IIdentityProviderService
            services.RemoveAll<IIdentityProviderService>();
            services.AddSingleton<IIdentityProviderService>(IdentityProviderFake);
        });
    }

    public override async ValueTask DisposeAsync()
    {
        await RabbitContainer.DisposeAsync();
        await base.DisposeAsync();
    }
}