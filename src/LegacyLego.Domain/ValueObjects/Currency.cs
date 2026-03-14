using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class Currency : ValueObject
{
    private static readonly Dictionary<string, Currency> Codes;

    public static readonly Currency Usd = new("USD", "$");

    public static readonly Currency Eur = new("EUR", "€");

    public static readonly Currency Rub = new("RUB", "₽");

    public string Code { get; }

    public string Symbol { get; }

    static Currency()
    {
        Codes = new Dictionary<string, Currency>()
        {
            { Usd.Code, Usd},
            { Eur.Code, Eur},
            { Rub.Code, Rub}
        };
    }

    private Currency(string code, string symbol)
    {
        Code = code;
        Symbol = symbol;
    }

    public static Result<Currency> FromCode(string code)
    {
        var codeString = code.ToUpperInvariant();

        if (codeString.Length != 3)
            return Result<Currency>.Failure(
                CurrencyErrors.GetWrongCodeError(codeString.Length, codeString));

        if (!Codes.TryGetValue(codeString, out var currency))
            return Result<Currency>.Failure(
                CurrencyErrors.GetCurrencyNotSupportedError(codeString));

        return Result<Currency>.Success(currency);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}

