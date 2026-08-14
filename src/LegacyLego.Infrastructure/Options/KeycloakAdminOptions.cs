using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class KeycloakAdminOptions
{
    public const string SectionName = "KeycloakAdmin";

    [Required(ErrorMessage = "BaseUrl не может быть пустым.")]
    [Url(ErrorMessage = "BaseUrl должен быть валидным URL.")]
    public string BaseUrl { get; set; } = String.Empty;

    [Required(ErrorMessage = "Realm не может быть пустым.")]
    public string Realm { get; set; } = "master";

    [Required(ErrorMessage = "ClientId не может быть пустым.")]
    public string ClientId { get; set; } = string.Empty;

    [Required(ErrorMessage = "ClientId не может быть пустым.")]
    public string ClientSecret { get; set; } = string.Empty;
}