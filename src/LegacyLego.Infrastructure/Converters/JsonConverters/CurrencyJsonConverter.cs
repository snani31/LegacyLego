using LegacyLego.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.Converters.JsonConverters;

public sealed class CurrencyJsonConverter : JsonConverter<Currency>
{
    public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected string for Currency, but got {reader.TokenType}.");

        var code = reader.GetString();

        if (string.IsNullOrWhiteSpace(code))
            throw new JsonException("Currency code in JSON is null or empty.");

        var result = Currency.FromCode(code);

        if (result.IsFailure)
            throw new JsonException($"Failed to deserialize Currency: {result.Error.Message}");

        return result.Value;
    }

    public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Code);
    }
}