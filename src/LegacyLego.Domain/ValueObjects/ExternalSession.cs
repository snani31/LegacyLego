using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using static System.Net.WebRequestMethods;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ExternalSession : ValueObject
{
    public string ExternalId { get; }
    public string CheckoutUrl { get; }
    public DateTime? ExpiresAtUtc { get; }

    private ExternalSession(string externalId, string checkoutUrl, DateTime? expiresAtUtc)
    {
        ExternalId = externalId;
        CheckoutUrl = checkoutUrl;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static Result<ExternalSession> Create(string externalId, string checkoutUrl, DateTime? expiresAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalId, nameof(externalId));
        ArgumentException.ThrowIfNullOrWhiteSpace(checkoutUrl, nameof(checkoutUrl));

        return Result<ExternalSession>.Success(new ExternalSession(externalId, checkoutUrl, expiresAtUtc));
    }

    public bool IsExpired(DateTime nowUtc)
    {
        if (ExpiresAtUtc is null) return false;

        return ExpiresAtUtc.Value <= nowUtc;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ExternalId;
        yield return CheckoutUrl;
        if (ExpiresAtUtc.HasValue) yield return ExpiresAtUtc.Value;
    }
}