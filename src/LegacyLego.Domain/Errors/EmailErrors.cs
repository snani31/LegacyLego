using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class EmailErrors
{
    public const string CreateValueRegexFailureCode = "Email.CreateValueRegexInvalidFormat";

    public const string CreateValueTooLongCode = "Email.CreateValueTooLong";


    public static Error GetCreateValueRegexInvalidFormatError(string value)
    {
        return new(
            Code: CreateValueRegexFailureCode,
            Message: $"Regex Error of value: {value}");
    }
    public static Error GetCreateValueTooLongError(int actualLength)
    {
        return new(
            Code: CreateValueTooLongCode,
            Message: $"Max Email Value can't be longer then 256, but it was: {actualLength}");
    }
}