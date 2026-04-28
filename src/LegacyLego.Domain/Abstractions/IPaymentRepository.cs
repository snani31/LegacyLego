using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IPaymentRepository
{
    public Task<OrderPayment?> GetByTransactionIdAsync(string id, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetPendingByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<bool> ExistsSucceeded(OrderId orderId);

    public Task<OrderPayment?> GetByIdAsync(OrderPaymentId orderId, CancellationToken cancellationToken = default);

    public void Add(OrderPayment payment);
}