using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Services;

public sealed class PaymentLookup
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly TimeProvider _timeProvider;

    public PaymentLookup(IPaymentRepository paymentRepository, TimeProvider timeProvider)
    {
        _paymentRepository = paymentRepository;
        _timeProvider = timeProvider;
    }

    public async Task<OrderPayment> GetOrCreateAsync(
        string transactionId,
        OrderId orderId,
        CancellationToken ct)
    {
        var payment = await _paymentRepository
            .GetByTransactionIdAsync(transactionId, ct);

        if (payment is not null)
            return payment;

        payment = await _paymentRepository
            .GetByOrderIdAsync(orderId, ct);

        if (payment is not null)
            return payment;

        var now = _timeProvider.GetUtcNow().UtcDateTime;
        var createResult = OrderPayment.Create(orderId, now);

        if (createResult.IsFailure)
            throw new InvalidOperationException(
                $"Failed to create OrderPayment: {createResult.Error}");

        payment = createResult.Value;

        _paymentRepository.Add(payment);

        return payment;
    }
}