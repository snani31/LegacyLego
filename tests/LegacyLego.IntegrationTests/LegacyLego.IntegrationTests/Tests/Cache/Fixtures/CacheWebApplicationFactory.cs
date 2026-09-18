using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Options;
using LegacyLego.IntegrationTests.Infrastructure;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using LegacyLego.IntegrationTests.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace LegacyLego.IntegrationTests.Tests.Cache.Fixtures;

public class CacheWebApplicationFactory : BaseWebApplicationFactory
{
    protected readonly PostgreSqlContainer DbContainer = new PostgreSqlBuilder("postgres:16-alpine")
        .WithDatabase("legacy_lego_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected readonly RedisContainer RedisContainer = new RedisBuilder("redis:7-alpine")
        .Build();

    public IConnectionMultiplexer Redis => Services.GetRequiredService<IConnectionMultiplexer>();

    public override async Task InitializeAsync()
    {
        await DbContainer.StartAsync();
        await RedisContainer.StartAsync();

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

            services.RemoveAll<IConnectionMultiplexer>();
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(RedisContainer.GetConnectionString()));
        });
    }

    public override async ValueTask DisposeAsync()
    {
        await DbContainer.DisposeAsync();
        await RedisContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    public async Task<string?> GetRedisStringAsync(string key)
    {
        var db = Redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.HasValue ? value.ToString() : null;
    }

    public async Task<bool> KeyExistsInRedisAsync(string key)
    {
        var db = Redis.GetDatabase();
        return await db.KeyExistsAsync(key);
    }
}
