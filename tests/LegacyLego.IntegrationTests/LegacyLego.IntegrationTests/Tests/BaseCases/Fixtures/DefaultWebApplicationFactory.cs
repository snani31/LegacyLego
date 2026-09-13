using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Options;
using LegacyLego.IntegrationTests.Infrastructure;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using LegacyLego.IntegrationTests.Infrastructure.Extensions;
using LegacyLego.IntegrationTests.Infrastructure.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;

public class DefaultWebApplicationFactory : BaseWebApplicationFactory
{
    protected readonly PostgreSqlContainer DbContainer = new PostgreSqlBuilder("postgres:16-alpine")
        .WithDatabase("legacy_lego_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public override async Task InitializeAsync()
    {
        await DbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        await dbContext.Database.MigrateAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveHostedServices();
            services.AddStubCacheInvalidator();
            services.AddTestAuth();

            services.Configure<DatabaseOptions>(options =>
            {
                var builder = new NpgsqlConnectionStringBuilder(DbContainer.GetConnectionString())
                {
                    IncludeErrorDetail = true
                };

                options.ConnectionString = builder.ConnectionString;
                options.EnableDetailedErrors = true;
                options.EnableSensitiveDataLogging = true;

            });
        });
    }

    public override async ValueTask DisposeAsync()
    {
        await DbContainer.DisposeAsync();
        await base.DisposeAsync();
    }
}
