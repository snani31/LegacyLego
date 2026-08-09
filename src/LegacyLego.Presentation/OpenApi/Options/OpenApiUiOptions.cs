using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Presentation.OpenApi.Options;

public sealed class OpenApiUiOptions
{
    public const string SectionName = "OpenApiUiOptions";

    [Required(ErrorMessage = "Title документации не может быть пустым.")]
    public string Title { get; set; } = "LegacyLego Documentation";

    [Required(ErrorMessage = "RoutePrefix не может быть пустым.")]
    public string RoutePrefix { get; set; } = "/docs/scalar";

    [Required(ErrorMessage = "ClientId не может быть пустым.")]
    public string ClientId { get; set; } = "legacylego-api";
}