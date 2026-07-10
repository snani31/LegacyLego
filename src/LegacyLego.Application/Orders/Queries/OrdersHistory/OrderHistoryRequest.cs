namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrderHistoryRequest(
    string? Cursor = null   // Base64 токен
);