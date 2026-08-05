using LegacyLego.Infrastructure.Converters.JsonConverters;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Options;

public static class OutboxSerializerOptions
{
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        Converters =
        {
            new CurrencyJsonConverter(),
            new PriceJsonConverter()
        }
    };
}