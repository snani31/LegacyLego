using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class PriceExceptionalErrors
{
    public static ExceptionalError GetMultiplyBelowZeroError(
        int factor)
    {
        return new(
            Code: "Price.MultiplyBelowZero",
            Message: $"Множитель стоимости не должен опускаться ниже нуля, текущее значение {factor} нарушает доменную логику"
        );
    }

    public static ExceptionalError GetCurrencyMismatchError(
        string currencyCode,
        string otherCurrencyCode)
    {
        return new(
            Code: "Price.CurrencyMismatch",
            Message: $"Складывать значения различных валют недопустимо: " +
            $"({currencyCode} + {otherCurrencyCode}) считается недопустимой операцией, нарушающей доменную логику"
        );
    }
}