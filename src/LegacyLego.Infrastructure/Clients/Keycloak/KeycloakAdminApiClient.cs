using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Dto;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.Clients.Keycloak;

public class KeycloakAdminApiClient : IIdentityProviderService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _options;
    private readonly ILogger<KeycloakAdminApiClient> _logger;

    public KeycloakAdminApiClient(
        HttpClient httpClient,
        IOptions<KeycloakOptions> options,
        ILogger<KeycloakAdminApiClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ExternalUserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken ct = default)
    {
        // Получить Bearer token по схеме Client Credentials
        var token = await GetAccessTokenAsync(ct);

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"/admin/realms/{_options.Realm}/users/{userId}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //  Запрос к Keycloak Admin API
        var response = await _httpClient.SendAsync(request, ct);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Пользователь с ID {UserId} не найден в Keycloak", userId);
            return null;
        }

        response.EnsureSuccessStatusCode();

        var userDto = await response.Content.ReadFromJsonAsync<KeycloakUserResponseDto>(cancellationToken: ct);

        if (userDto is null)
            return null;

        return new ExternalUserProfile(
            UserId: userDto.Id,
            Username: userDto.Username,
            Email: userDto.Email,
            FirstName: userDto.FirstName,
            LastName: userDto.LastName,
            PhoneNumber: userDto.PhoneNumber,
            CreatedAtUtc: DateTimeOffset.FromUnixTimeMilliseconds(userDto.CreatedTimestamp).UtcDateTime
        );
    }

    /// <summary>
    /// Запрос служебного токена (Client Credentials Flow) для доступа к Admin API
    /// </summary>
    private async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        var tokenEndpoint = $"/realms/{_options.Realm}/protocol/openid-connect/token";

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _options.PrivateApiClientId),
            new KeyValuePair<string, string>("client_secret", _options.PrivateApiClientSecret)
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, content, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(cancellationToken: ct);
        return result?.AccessToken ?? throw new InvalidOperationException("Не удалось получить токен доступа Keycloak.");
    }

    private record KeycloakTokenResponse([property: JsonPropertyName("access_token")] string AccessToken);

    private record KeycloakUserResponseDto(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("username")] string Username,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("firstName")] string? FirstName,
        [property: JsonPropertyName("lastName")] string? LastName,
        [property: JsonPropertyName("createdTimestamp")] long CreatedTimestamp,
        [property: JsonPropertyName("attributes")] Dictionary<string, string[]>? Attributes)
    {
        public string? PhoneNumber =>
            Attributes != null && Attributes.TryGetValue("phoneNumber", out var values)
                ? values.FirstOrDefault()
                : null;
    }
}