using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Sum { get; }

    public Currency Currency { get; }

    bool IsPositive => Sum > 0;

    bool IsZero => Sum == 0m;

    private Price(decimal sum,Currency currency)
    {
        Sum = Normalize(sum, currency.Scale);
        Currency = currency;   
    }

    private static decimal Normalize(decimal value, int scale)
    {
        return Math.Round(value, scale, MidpointRounding.ToEven);
    }

    public static Price Zero(Currency currency)
    {
        return new Price(0m, currency);
    }

    public static Result<Price> Create(decimal sum, Currency currency)
    {
        if (sum <= 0)
        {
            return Result<Price>.Failure(PriceErrors.GetSumBelowZeroError(sum));
        }

        var price = new Price(sum, currency);

        return Result<Price>.Success(price);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Sum;
    }

    public Price Plus(Price other)
    {
        if (!this.Currency.Equals(other.Currency))
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetCurrencyMismatchError(
                    this.Currency.Code,other.Currency.Code));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum + other.Sum);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetAdditionSumOverflowError(this.Sum,other.Sum));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }

    public Price MultiplyByQuantity(int factor)
    {
        if (factor < 0)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetMultiplyBelowZeroError(factor));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum * factor);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(PriceExceptionalErrors
                .GetMultiplySumOverflowError(this.Sum, factor));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }
}