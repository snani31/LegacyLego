using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class PriceErrors
{
    public static Error GetSumBelowZeroError(
        decimal sum)
    {
        return new(
            Code: "Price.SumBelowZero",
            Message: $"значение цены не должно равняться нулю или опускаться ниже, текущее значение {sum} некорректно"
        );
    }
}
