using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class LanguageErrors
{
    public const string NotSupportedCode = "Language.NotSupported";

    public static Error GetNotSupportedError(string codeString)
    {
        return new(
            Code: NotSupportedCode,
            Message: $"Selected language code: {codeString} not identified");
    }
}