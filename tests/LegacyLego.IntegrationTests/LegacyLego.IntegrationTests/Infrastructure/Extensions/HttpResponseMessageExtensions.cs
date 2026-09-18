using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.IntegrationTests.Infrastructure.Extensions;

public static class HttpResponseMessageExtensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public static Task<T?> ReadJsonAsync<T>(this HttpResponseMessage response, JsonSerializerOptions? options = null)
    {
        return response.Content.ReadFromJsonAsync<T>(options ?? DefaultOptions);
    }
}