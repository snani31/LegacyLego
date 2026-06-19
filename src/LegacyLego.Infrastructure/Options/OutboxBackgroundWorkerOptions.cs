using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class OutboxBackgroundWorkerOptions
{
    public const string SectionName = "OutboxBackgroundWorkerOptions";

    [Range(1, 60, ErrorMessage = "Период воркера должен быть от 1 до 60 секунд.")]
    public int SecondsPeriod { get; set; } = 2;

    [Range(1, 100, ErrorMessage = "За раз можно взять от 1 до 100 записей.")]
    public int TakeRecordsNum { get; set; } = 20;
}