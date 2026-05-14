using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class ExternalSessionExceptionalErrors
{
    public const string IsExpiredCompressionParameterIsNotUtcCode = "ExternalSession.IsExpiredCompressionParameterIsNotUtcCode ";

    public static ExceptionalError GetIsExpiredCompressionParameterIsNotUtcError(DateTimeKind timeKind)
    {
        return new(
            Code: IsExpiredCompressionParameterIsNotUtcCode,
            Message: $"Тип передаваемого времени передаваемого параметра в метод IsExpired должен быть представлен Utc, но был {timeKind}");
    }
}