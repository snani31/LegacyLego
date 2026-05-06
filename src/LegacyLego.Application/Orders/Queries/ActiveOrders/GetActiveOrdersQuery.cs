using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetActiveOrdersQuery(Guid UserId) : IQuery<IReadOnlyList<OrderSummaryDto>>;