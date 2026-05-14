using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ExternalSession : ValueObject
{
    public string ExternalId { get; }
    public string CheckoutUrl { get; }
    public DateTime ExpiresAtUtc { get; }

    private ExternalSession(string externalId, string checkoutUrl, DateTime expiresAtUtc)
    {
        ExternalId = externalId;
        CheckoutUrl = checkoutUrl;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static Result<ExternalSession> Create(string externalId, string checkoutUrl, DateTime expiresAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalId, nameof(externalId));
        ArgumentException.ThrowIfNullOrWhiteSpace(checkoutUrl, nameof(checkoutUrl));

        if (expiresAtUtc.Kind is not DateTimeKind.Utc)
            return Result<ExternalSession>.Failure(
                ExternalSessionErrors.GetExpirationTimeWasNotUtceError(expiresAtUtc.Kind));

        return Result<ExternalSession>.Success(new ExternalSession(externalId, checkoutUrl, expiresAtUtc));
    }

    public bool IsExpired(DateTime nowUtc)
    {
        if (nowUtc.Kind is not DateTimeKind.Utc)
        {
            throw new InvariantViolationException(ExternalSessionExceptionalErrors.GetIsExpiredCompressionParameterIsNotUtcError(nowUtc.Kind));
        }

        return ExpiresAtUtc <= nowUtc;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ExternalId;
        yield return CheckoutUrl;
        yield return ExpiresAtUtc;
    }
}