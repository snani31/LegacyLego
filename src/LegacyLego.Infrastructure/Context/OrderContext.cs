using LegacyLego.Domain.Aggregates;
using LegacyLego.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Context;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderPayment> OrderPayments { get; set; } = null!;

    public OrderContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalSessionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}