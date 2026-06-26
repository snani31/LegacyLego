using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class PaymentProviderOptions
{
    public const string SectionName = "PaymentProviderOptions";

    [Required(ErrorMessage = "ApiBaseUrl is required")]
    public string ApiBaseUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "WebhookRoute is required")]
    public string WebhookRoute { get; set; } = string.Empty;

    [Required(ErrorMessage = "CheckoutPagePath is required")]
    public string CheckoutPagePath { get; set; } = string.Empty;

    [Range(1, 60, ErrorMessage = "ExpiresAtMinutes должен быть от 1 до 60 минут.")]
    public int ExpiresAtMinutes { get; set; } = 10;
}