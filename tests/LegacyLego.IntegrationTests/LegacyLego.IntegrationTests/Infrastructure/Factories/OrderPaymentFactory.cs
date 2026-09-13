using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.IntegrationTests.Infrastructure.Factories;

public static class OrderPaymentFactory
{
    public static OrderPayment CreatePendingPayment(
        OrderId orderId,
        Price expectedAmount,
        string? externalSessionId = null)
    {
        var now = DateTime.UtcNow;
        var paymentResult = OrderPayment.Create(orderId, expectedAmount, now);

        if (paymentResult.IsFailure)
        {
            throw new InvalidOperationException($"Не удалось создать OrderPayment: {paymentResult.Error.Message}");
        }

        var payment = paymentResult.Value;
        var sessionId = externalSessionId ?? $"ext_{Guid.NewGuid():N}";

        // Создаем тестовую сессию с запасом по времени жизни
        var sessionResult = ExternalSession.Create(
            externalId: sessionId,
            checkoutUrl: "https://checkout.mock.local/pay",
            expiresAtUtc: now.AddMinutes(15));

        payment.AttachSession(sessionResult.Value, now);

        return payment;
    }
}