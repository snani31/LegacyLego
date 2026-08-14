using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class PhoneNumberErrors
{
    public const string CreateValueRegexFailureCode = "PhoneNumber.CreateValueRegexInvalidFormat";

    public static Error GetCreateValueRegexInvalidFormatError(string value)
    {
        return new(
            Code: CreateValueRegexFailureCode,
            Message: $"Regex Error of value: {value}");
    }
}