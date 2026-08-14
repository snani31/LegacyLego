using LegacyLego.Domain.Shared;
using System.Text.Json.Serialization;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ClientPreferences : ValueObject
{
    public string LanguageCode { get; init; }
    public string CurrencyCode { get; init; } 

    [JsonConstructor]
    private ClientPreferences(string languageCode, string currencyCode)
    {
        LanguageCode = languageCode;
        CurrencyCode = currencyCode;
    }

    public static Result<ClientPreferences> Create(Language language, Currency currency)
    {
        ArgumentNullException.ThrowIfNull(language, nameof(language));
        ArgumentNullException.ThrowIfNull(currency, nameof(currency));

        return Result<ClientPreferences>.Success(
            new ClientPreferences(language.Code, currency.Code));
    }

    public static ClientPreferences Default => new(Language.Russian.Code, Currency.Rub.Code);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return LanguageCode;
        yield return CurrencyCode;
    }
}