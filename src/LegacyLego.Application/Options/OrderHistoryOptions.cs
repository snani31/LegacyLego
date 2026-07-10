using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Application.Options;

public class OrderHistoryOptions
{
    public const string SectionName = "OrderHistory";

    [Range(1, 15, ErrorMessage = "Значение OrderHistory PageSize должно быть в диапазоне от 1 до 15.")]
    public int PageSize { get; set; } = 5;

}