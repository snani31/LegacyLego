using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class CacheOptions
{
    public const string SectionName = "CacheOptions";

    [Range(10, 30, ErrorMessage = "OrdersHistoryTtl должен быть от 10 до 30 минут.")]
    public int OrdersHistoryMinutesTtl { get; set; } = 10;

    [Range(10, 60, ErrorMessage = "OrderDetailsTtl должен быть от 10 до 60 минут.")]
    public int OrderDetailsMinutesTtl { get; set; } = 30;

    [Range(1, 7, ErrorMessage = "OrderGroupDaysTtl должен быть от 1 до 7 дней.")]
    public int OrderGroupDaysTtl { get; set; } = 1;
}
