using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LegacyLego.Infrastructure.Design;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Database=dummy;Username=dummy;Password=dummy");

        return new OrderContext(optionsBuilder.Options);
    }
}