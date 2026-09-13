using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LegacyLego.IntegrationTests.Infrastructure.Messaging;

public static class RabbitMqTestExtensions
{
    public static async Task PublishEventAsync<TEvent>(
        this KeycloakRegistrationWebApplicationFactory factory,
        string exchange,
        string routingKey,
        TEvent eventPayload)
    {
        var connectionFactory = factory.Services.GetRequiredService<IConnectionFactory>();

        using var connection = await connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        var json = JsonSerializer.Serialize(eventPayload);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: false,
            body: body);
    }
}