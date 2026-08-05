using LegacyLego.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.Converters.JsonConverters
{
    public sealed class PriceJsonConverter : JsonConverter<Price>
    {
        public override Price Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException($"Expected StartObject for Price, but got {reader.TokenType}.");

            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!TryGetProperty(root, "Sum", "sum", out var sumElement) || !sumElement.TryGetDecimal(out var sum))
                throw new JsonException("Property 'Sum' is missing or not a valid decimal.");

            if (!TryGetProperty(root, "Currency", "currency", out var currencyElement))
                throw new JsonException("Property 'Currency' is missing.");

            var currency = JsonSerializer.Deserialize<Currency>(currencyElement.GetRawText(), options)
                           ?? throw new JsonException("Failed to deserialize Currency inside Price.");

            var result = Price.Create(sum, currency);

            if (result.IsFailure)
                throw new JsonException($"Failed to create Price during deserialization: {result.Error.Message}");

            return result.Value;
        }

        public override void Write(Utf8JsonWriter writer, Price value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            var sumPropertyName = options.PropertyNamingPolicy?.ConvertName("Sum") ?? "Sum";
            var currencyPropertyName = options.PropertyNamingPolicy?.ConvertName("Currency") ?? "Currency";

            writer.WriteNumber(sumPropertyName, value.Sum);

            writer.WritePropertyName(currencyPropertyName);
            JsonSerializer.Serialize(writer, value.Currency, options);

            writer.WriteEndObject();
        }

        private static bool TryGetProperty(JsonElement element, string pascalName, string camelName, out JsonElement value)
        {
            if (element.TryGetProperty(pascalName, out value)) return true;
            if (element.TryGetProperty(camelName, out value)) return true;
            return false;
        }
    }
}
