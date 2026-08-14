using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class ClientErrors
{
    public const string CreationTimeWasNotUtcCode = "Client.CreationTimeWasNotUtc";

    public const string CreateLastNameInvalieLengthCode = "Client.CreateLastNameInvalieLength";
    public const string CreateFirstNameInvalieLengthCode = "Client.CreateFirstNameInvalieLength";

    public static Error GetCreationTimeWasNotUtcError(DateTime createdAt)
    {
        return new(
            Code: CreationTimeWasNotUtcCode,
            Message: $"Client.Create inserted createdAt value: {createdAt} DateTimeType is not expected Utc. It was: {createdAt.Kind}");
    }

    public static Error GetCreateLastNameInvalieLengthError(string value)
    {
        return new(
            Code: CreateLastNameInvalieLengthCode,
            Message: $"Client.Create inserted lastName value: {value} has unexpected Length. It was: {value.Length}");
    }

    public static Error GetCreateFirstNameInvalieLengthError(string value)
    {
        return new(
            Code: CreateFirstNameInvalieLengthCode,
            Message: $"Client.Create inserted firstName value: {value} has unexpected Length. It was: {value.Length}");
    }

}