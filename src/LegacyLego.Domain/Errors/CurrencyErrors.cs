using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class CurrencyErrors
{
    public static Error GetWrongCodeError(
        int actualCodeLength,
        string codeString)
    {
        return new(
            Code: "Currency.WrongCode",
            Message: $"Код валюты должен состоять ровно из 3 символов. Код {codeString} содержит {actualCodeLength}");
    }

    public static Error GetNotSupportedError(string codeString)
    {
        return new(
            Code: "Currency.NotSupported",
            Message: $"Выбранная вами валюта {codeString} не поддерживается системой");
    }
}