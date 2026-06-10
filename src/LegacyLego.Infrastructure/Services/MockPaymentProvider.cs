using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace LegacyLego.Infrastructure.Services;

public sealed class MockPaymentProvider : IPaymentProvider
{
    private const string HTTP_ROUTE = "/mock/api/webhooks/payment";

    private readonly HttpClient _httpClient;
    private readonly TimeProvider _timeProvider;
    private readonly string _baseUrl;
    private readonly int _expiresAtMinutes;

    public MockPaymentProvider(
        HttpClient httpClient,
        TimeProvider timeProvider,
        string baseUrl,
        int expiresAtMinutes)
    {
        _httpClient = httpClient;
        _timeProvider = timeProvider;
        _baseUrl = baseUrl;
        _expiresAtMinutes = expiresAtMinutes;
    }

    public async Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        CancellationToken ct)
    {
        string externalSessionId = GenerateExternalSession();

        var expiresAtUtc = _timeProvider.GetUtcNow().AddMinutes(_expiresAtMinutes).UtcDateTime;

        var queryParams = new Dictionary<string, string?>
        {
            { "paymentId", paymentId.ToString() },
            { "orderId", orderId.ToString() },
            { "amount", amount.ToString("F2") },
            { "currency", currency },
            { "externalSessionId", externalSessionId }
        };

        string checkoutUrl = QueryHelpers.AddQueryString(_baseUrl, queryParams);

        var session = new PaymentSession(
            PaymentId: paymentId,
            ExternalSessionId: externalSessionId,
            CheckoutUrl: checkoutUrl,
            ExpiresAtUtc: expiresAtUtc
        );

        return Result<PaymentSession>.Success(session);
    }

    public async Task<Result> RequestRefundAsync(
        Guid orderId,
        decimal amount,
        string currency,
        string transactionId,
        CancellationToken ct)
    {
        await Task.Delay(500, ct);

        var payload = new ExternalStripeWebhookSimulation(
            OrderId: orderId,
            Amount: amount,
            Currency: currency,
            TransactionId: transactionId,
            Status: "refund");

        var response = await _httpClient.PostAsJsonAsync(HTTP_ROUTE, payload, ct);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(new Error(
                "MockPayment.RefundFailed",
                $"Имитация вебхука возврата завершилась ошибкой: {response.StatusCode}"));
        }

        return Result.Success();
    }

    private string GenerateExternalSession() =>  $"ext_sess_{Guid.NewGuid():N}";


}

file sealed record ExternalStripeWebhookSimulation(
        Guid OrderId,
        decimal Amount,
        string Currency,
        string TransactionId,
        string Status);