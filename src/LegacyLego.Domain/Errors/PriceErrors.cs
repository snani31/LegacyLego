using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class PriceErrors
{
    public const string SumBelowZeroCode = $"Price.SumBelowZero";
    public static Error GetSumBelowZeroError(
        decimal sum)
    {
        return new(
            Code: SumBelowZeroCode,
            Message: $"значение цены не должно равняться нулю или опускаться ниже, текущее значение {sum} некорректно"
        );
    }
}