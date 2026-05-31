using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Common;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Repositories;

internal class OrderRepository(OrderContext context) : IOrderRepository
{
    public void Add(Order order) => context.Orders.Add(order);

    public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        return await context.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<TResult?> GetOrderAsync<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           , specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> GetOrdersAsync<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           ,specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetOrdersCountAsync(Specification<Order, OrderId> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           , specification)
            .CountAsync(cancellationToken);
    }
}
