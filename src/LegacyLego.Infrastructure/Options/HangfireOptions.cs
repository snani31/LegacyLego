using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class HangfireOptions
{
    public const string SectionName = "Hangfire";
    public const string CommandHangfireQueueName = "command";

    [Range(1, 60, ErrorMessage = "QueuePollInterval должен быть от 1 до 60 секунд.")]
    public int QueuePollInterval { get; set; } = 15;

    [Range(1, 20, ErrorMessage = "WorkerCount должен быть в диапазоне от 1 до 20.")]
    public int WorkerCount { get; set; } = 2;
}