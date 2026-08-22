using LegacyLego.Domain.Aggregates;
using LegacyLego.Infrastructure.Configuration;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Context;

public class OrderContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderPayment> OrderPayments => Set<OrderPayment>();

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public OrderContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalSessionConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}