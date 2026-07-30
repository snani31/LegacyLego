using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Repositories;

internal class PaymentRepository(OrderContext context) : IPaymentRepository
{
    public void Add(OrderPayment payment) => context.OrderPayments.Add(payment);

    public async Task<bool> ExistsSucceededAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
    await context.OrderPayments
        .AnyAsync(p => p.OrderId == orderId && p.Status == PaymentStatus.Succeeded, cancellationToken);

    public async Task<OrderPayment?> GetByOrderIdAsync(OrderId orderId,
        CancellationToken cancellationToken = default) => 
        await context.OrderPayments.FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);

    public async Task<OrderPayment?> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken = default) =>
        await context.OrderPayments.FirstOrDefaultAsync(p => p.TransactionId == transactionId, cancellationToken);

    public async Task<OrderPayment?> GetPendingByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await context.OrderPayments.Where(p => p.OrderId == orderId && p.Status == PaymentStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<OrderPayment?> GetByExternalSessionIdAsync(
        string externalSessionId, CancellationToken cancellationToken = default) =>
         await context.OrderPayments.FirstOrDefaultAsync(p => p.ExternalSession != null && p.ExternalSession.ExternalId == externalSessionId, cancellationToken);
}
