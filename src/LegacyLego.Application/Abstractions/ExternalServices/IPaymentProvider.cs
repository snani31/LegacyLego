using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IPaymentProvider
{
    public Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        CancellationToken ct);

    Task<Result> RequestRefundAsync(
        Guid orderId,
        decimal amount,
        string currency,
        string transactionId,
        CancellationToken ct);
}