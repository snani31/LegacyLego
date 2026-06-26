using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace LegacyLego.Infrastructure.Services;

public sealed class MockPaymentProvider : IPaymentProvider
{
    private readonly HttpClient _httpClient;
    private readonly TimeProvider _timeProvider;
    private readonly PaymentProviderOptions _options;

    public MockPaymentProvider(
        HttpClient httpClient,
        TimeProvider timeProvider,
        IOptions<PaymentProviderOptions> options)
    {
        _httpClient = httpClient;
        _timeProvider = timeProvider;
        _options = options.Value;
    }

    public async Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        int scale,
        CancellationToken ct)
    {
        string externalSessionId = GenerateExternalSession();

        var expiresAtUtc = _timeProvider.GetUtcNow().AddMinutes(_options.ExpiresAtMinutes).UtcDateTime;

        var queryParams = new Dictionary<string, string?>
        {
            { "paymentId", paymentId.ToString() },
            { "orderId", orderId.ToString() },
            { "amount", amount.ToString($"F{scale}",System.Globalization.CultureInfo.InvariantCulture) },
            { "currency", currency },
            { "externalSessionId", externalSessionId }
        };

        string baseCheckoutUrl = new Uri(new Uri(_options.ApiBaseUrl), _options.CheckoutPagePath).ToString();
        string checkoutUrl = QueryHelpers.AddQueryString(baseCheckoutUrl, queryParams);

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

        var response = await _httpClient.PostAsJsonAsync(_options.WebhookRoute, payload, ct);

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