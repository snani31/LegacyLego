using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class JwtOptions
{
    public const string SectionName = "Authentication";

    [Required(ErrorMessage = "Authority не может быть пустым.")]
    [Url(ErrorMessage = "Authority должен быть валидным URL.")]
    public string Authority { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidIssuer не может быть пустым.")]
    public string ValidIssuer { get; set; } = string.Empty;

    public bool RequireHttpsMetadata { get; set; } = true;
}