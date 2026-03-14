using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Sum { get; }

    public Currency Currency { get; }

    private Price(decimal sum,Currency currency)
    {
        Sum = sum;
        Currency = currency;   
    }

    public static Price Zero(Currency currency)
    {
        return new Price(0, currency);
    }

    public static Result<Price> FromCode(decimal sum, string currencyCode)
    {
        if (sum <= 0)
        {
            return Result<Price>.Failure(PriceErrors.GetSumBelowZeroError(sum));
        }

        var currencyResult = Currency.FromCode(currencyCode);

        if (currencyResult.IsFailure)
        {
            return Result<Price>.Failure(currencyResult.Error);
        }

        var price = new Price(sum, currencyResult.Value);

        return Result<Price>.Success(price);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Sum;
        yield return Currency;
    }

    public Price Plus(Price other)
    {
        if (!this.Currency.Equals(other.Currency))
        {
            // TODO DomainException
            throw new Exception();
        }

        var sum = this.Sum + other.Sum;
        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }

    public Price Multiply(int factor)
    {
        if (factor < 0)
        {
            // TODO DomainException
            throw new Exception();
        }

        var sum = this.Sum * factor;
        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }
}
