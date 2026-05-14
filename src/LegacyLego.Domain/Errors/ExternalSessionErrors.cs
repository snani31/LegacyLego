using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class ExternalSessionErrors
{
    public const string ExpirationTimeWasNotUtcCode= "ExternalSession.ExpirationTimeWasNotUtc";

    public static Error GetExpirationTimeWasNotUtceError(DateTimeKind timeKind)
    {
        return new(
            Code: ExpirationTimeWasNotUtcCode,
            Message: $"Тип передаваемого времени должен быть представлен Utc, но был {timeKind}");
    }
}