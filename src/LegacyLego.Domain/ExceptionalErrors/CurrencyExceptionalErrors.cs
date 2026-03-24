using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class CurrencyExceptionalErrors
{
    public static ExceptionalError GetInvalidScaleValueError(int actualScale)
    {
        return new(
            Code: "Currency.InvalidScaleValue",
            Message: $"Scale валюты не должен опускаться ниже нуля, значение: {actualScale} нарушает целостность системы");
    }
}