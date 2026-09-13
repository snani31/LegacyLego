using LegacyLego.Infrastructure.Context;
using LegacyLego.IntegrationTests.Infrastructure;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace LegacyLego.IntegrationTests.Tests;

public abstract class BaseIntegrationTest<TFactory> : IAsyncDisposable
    where TFactory : BaseWebApplicationFactory
{
    private readonly IServiceScope _scope;

    // Теперь Factory имеет точный тип конкретной фабрики!
    protected readonly TFactory Factory;

    protected readonly HttpClient Client;
    protected readonly OrderContext DbContext;

    protected BaseIntegrationTest(TFactory factory)
    {
        Factory = factory;
        Client = CreateClient();

        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<OrderContext>();
    }

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