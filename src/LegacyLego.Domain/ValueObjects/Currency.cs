using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Shared;
using System.IO.IsolatedStorage;

namespace LegacyLego.Domain.ValueObjects;

public class Currency : ValueObject
{
    private static readonly Dictionary<string, Currency> Codes;

    public static readonly Currency Usd = new("USD", "$", 2);

    public static readonly Currency Eur = new("EUR", "€", 2);

    public static readonly Currency Rub = new("RUB", "₽", 2);

    public string Code { get; }

    public string Symbol { get; }

    public int Scale { get; }

    static Currency()
    {
        Codes = new Dictionary<string, Currency>()
        {
            { Usd.Code, Usd},
            { Eur.Code, Eur},
            { Rub.Code, Rub}
        };
    }

    private Currency(string code, string symbol, int scale = 2)
    {
        if (scale < 0)
           throw new InvariantViolationException(
                CurrencyExceptionalErrors.GetInvalidScaleValueError(scale));

        Code = code.ToUpperInvariant();
        Symbol = symbol;
        Scale = scale;

    }

    public static Result<Currency> FromCode(string code)
    {
        if(code is null) 
            throw new ArgumentNullException(nameof(code));

        var codeString = code.Trim().ToUpperInvariant();

        if (codeString.Length != 3)
            return Result<Currency>.Failure(
                CurrencyErrors.GetWrongCodeError(codeString.Length, codeString));

        if (!Codes.TryGetValue(codeString, out var currency))
            return Result<Currency>.Failure(
                CurrencyErrors.GetNotSupportedError(codeString));

        var scale = currency.Scale;


        return Result<Currency>.Success(currency);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}