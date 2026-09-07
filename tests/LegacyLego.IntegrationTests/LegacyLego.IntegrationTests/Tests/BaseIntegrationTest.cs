using LegacyLego.Infrastructure.Context;
using LegacyLego.IntegrationTests.Infrastructure;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace LegacyLego.IntegrationTests.Tests;

public abstract class BaseIntegrationTest : IAsyncDisposable
{
    private readonly IServiceScope _scope;
    protected readonly BaseWebApplicationFactory Factory;

    // Стандартный клиент с ролью Client по умолчанию
    protected readonly HttpClient Client;
    protected readonly OrderContext DbContext;

    protected BaseIntegrationTest(BaseWebApplicationFactory factory)
    {
        Factory = factory;
        Client = CreateClient(); // Дефолтный клиент (Role = Client)

        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<OrderContext>();
    }

    /// <summary>
    /// Создает авторизованный HttpClient с возможностью переопределить роль и UserId
    /// </summary>
    protected HttpClient CreateClient(string? role = null, Guid? userId = null)
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.SchemeName);

        if (!string.IsNullOrEmpty(role))
        {
            client.DefaultRequestHeaders.Add(TestAuthHandler.RoleHeader, role);
        }

        if (userId.HasValue)
        {
            client.DefaultRequestHeaders.Add(TestAuthHandler.UserIdHeader, userId.Value.ToString());
        }

        return client;
    }

    /// <summary>
    /// Создает чистый неавторизованный HttpClient (без заголовка Authorization)
    /// </summary>
    protected HttpClient CreateUnauthenticatedClient()
    {
        return Factory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        DbContext.Dispose();
        _scope.Dispose();
        await Task.CompletedTask;
    }
}