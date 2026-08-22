using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class KeycloakOptions
{
    public const string SectionName = "Keycloak";

    [Required(ErrorMessage = "BaseUrl не может быть пустым.")]
    [Url(ErrorMessage = "BaseUrl должен быть валидным URL.")]
    public string BaseUrl { get; set; } = String.Empty;

    [Required(ErrorMessage = "Realm не может быть пустым.")]
    public string Realm { get; set; } = "master";

    [Required(ErrorMessage = "PublicClientId не может быть пустым.")]
    public string PublicClientId { get; set; } = string.Empty;

    [Required(ErrorMessage = "PrivateApiClientId не может быть пустым.")]
    public string PrivateApiClientId { get; set; } = string.Empty;

    [Required(ErrorMessage = "PrivateApiClientSecret не может быть пустым.")]
    public string PrivateApiClientSecret { get; set; } = string.Empty;

    public string RealmUrl => $"{BaseUrl.TrimEnd('/')}/realms/{Realm}";
    public string AuthorizationEndpoint => $"{RealmUrl}/protocol/openid-connect/auth";
}