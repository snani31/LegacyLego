using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IPaymentProvider
{
    public Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        decimal amount,
        string currency,
        CancellationToken ct);

    public Task<Result<PaymentSession>> GetExistingSessionAsync(
        Guid paymentId,
        CancellationToken ct);
}