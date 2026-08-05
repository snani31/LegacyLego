using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace LegacyLego.Infrastructure.Design;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        if (environment == "Development")
        {
            builder.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
        }

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("Database")
                               ?? configuration["Database:ConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
            connectionString = "Host=localhost;Database=dummy;Username=dummy;Password=dummy";

        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new OrderContext(optionsBuilder.Options);
    }
}