using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class KeycloakOptions
{
    public const string SectionName = "Keycloak";

    // Внутренний URL для Backchannel-запросов (API -> Keycloak в сети Docker)
    [Required, Url]
    public string InternalBaseUrl { get; set; } = string.Empty;

    // Публичный URL для Frontchannel-запросов (Браузер -> Nginx -> Keycloak)
    [Required, Url]
    public string PublicBaseUrl { get; set; } = string.Empty;

    [Required]
    public string Realm { get; set; } = "master";

    [Required]
    public string PublicClientId { get; set; } = string.Empty;

    [Required]
    public string PrivateApiClientId { get; set; } = string.Empty;

    [Required]
    public string PrivateApiClientSecret { get; set; } = string.Empty;

    // Публичные эндпоинты (для браузера)
    public string PublicRealmUrl => $"{PublicBaseUrl.TrimEnd('/')}/realms/{Realm}";
    public string AuthorizationEndpoint => $"{PublicRealmUrl}/protocol/openid-connect/auth";
    public string LogoutEndpoint => $"{PublicRealmUrl}/protocol/openid-connect/logout";

    // Внутренние эндпоинты (для IHttpClientFactory)
    public string InternalRealmUrl => $"{InternalBaseUrl.TrimEnd('/')}/realms/{Realm}";
    public string TokenEndpoint => $"{InternalRealmUrl}/protocol/openid-connect/token";
    public string AdminUsersEndpoint => $"{InternalBaseUrl.TrimEnd('/')}/admin/realms/{Realm}/users";
}