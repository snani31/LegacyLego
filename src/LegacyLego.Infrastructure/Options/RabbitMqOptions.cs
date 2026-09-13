using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 5672;

    [Required(ErrorMessage = "Username не может быть пустым.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password не может быть пустым.")]
    public string Password { get; set; } = string.Empty;

    public string VirtualHost { get; set; } = "/";

    [Required(ErrorMessage = "KeycloakEventQueue не может быть пустым.")]
    public string KeycloakEventQueue { get; set; } = string.Empty;

    [Range(0, 10, ErrorMessage = "Количество ретраев должно быть от 0 до 10.")]
    public int RetryCount { get; set; } = 3;

    [Range(1, 60, ErrorMessage = "Интервал ретраев должен быть от 1 до 60 секунд.")]
    public int RetryIntervalSeconds { get; set; } = 1;
}