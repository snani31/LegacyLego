using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Services;

public sealed class PaymentLookup
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentLookup(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
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

        var createResult = OrderPayment.Create(orderId);

        if (createResult.IsFailure)
            throw new InvalidOperationException(
                $"Failed to create OrderPayment: {createResult.Error}");

        payment = createResult.Value;

        _paymentRepository.Add(payment);

        return payment;
    }
}