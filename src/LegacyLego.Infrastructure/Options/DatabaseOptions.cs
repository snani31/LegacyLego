using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required(ErrorMessage = "Строка подключения обязательна.")]
    public string ConnectionString { get; set; } = string.Empty;

    [Range(1, 60, ErrorMessage = "CommandTimeoutSeconds должен быть от 1 до 60 секунд.")]
    public int CommandTimeoutSeconds { get; set; } = 30;

    [Range(1, 10, ErrorMessage = "MaxRetryCount должен быть в диапазоне от 1 до 10.")]
    public int MaxRetryCount { get; set; } = 3;

    [Range(1, 30, ErrorMessage = "MaxRetryDelaySeconds должен быть в диапазоне от 1 до 30.")]
    public int MaxRetryDelaySeconds { get; set; } = 5;

    public bool EnableSensitiveDataLogging { get; set; }
    public bool EnableDetailedErrors { get; set; }
}