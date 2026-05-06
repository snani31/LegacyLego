namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrderHistoryRequest(
    int SkipRecords,
    int TakeRecords,
    decimal? MinPrice = null,
    string? SortBy = null,
    bool SortDescending = true);