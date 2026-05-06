using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IOrderRepository
{
    public Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Order>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<TResult>> GetOrders<TResult>(Specification<Order,OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<TResult?> GetOrder<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<int> GetOrdersCount(Specification<Order, OrderId> specification, CancellationToken cancellationToken = default);

    public void Add(Order order);
}