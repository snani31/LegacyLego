using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Commands.Create;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Presentation.Orders.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace LegacyLego.Presentation.Orders;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var ordersGroup = app.MapGroup("/orders")
            .WithDisplayName("Orders")
            .WithDescription("Управление заказами")
            .WithTags("Orders");

        ordersGroup.MapPost("", Create);
        ordersGroup.MapGet("/active", GetActiveOrders); // Сделали явный чистый эндпоинт
        ordersGroup.MapGet("/history", GetOrdersHistory);
        ordersGroup.MapGet("/{orderId:guid}", GetOrderDetails);

        return app;
    }

    private static async Task<Results<Created<Guid>, BadRequest<ProblemDetails>>> Create(
        [FromBody] CreateOrderRequest request,
        ICommandDispatcher commandDispatcher,
        ClaimsPrincipal user,
        ILogger<Program> logger,
        HttpContext httpContext,
        CancellationToken ct)
    {
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Временно для тестов, пока не настроен JWT:
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            clientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        var command = new CreateOrderCommand(
            ClientId: clientId,
            CurrencyCode: request.CurrencyCode,
            OrderAddress: request.OrderAddress,
            Items: request.Items
        );

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            logger.LogWarning("Запрос отклонен. Код: {ErrorCode}. Детали: {Message}",
                result.Error.Code, result.Error.Message);

            return TypedResults.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Произошла ошибка обработки запроса",
                Detail = "Запрос не прошел валидацию. Подробности см. в параметре errorCode.",
                Instance = httpContext.Request.Path,
                // Передаем код ошибки для фронтенда:
                Extensions = { ["errorCode"] = result.Error.Code }
            });
        }

        return TypedResults.Created($"/orders/{result.Value}", result.Value);
    }

    private static async Task<IResult> GetActiveOrders(
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetActiveOrdersQuery(GetClientId(user));
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static async Task<IResult> GetOrdersHistory(
        [AsParameters] OrderHistoryRequest request,
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetOrdersHistoryQuery(GetClientId(user), request);
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static async Task<IResult> GetOrderDetails(
        [FromRoute] Guid orderId,
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetOrderDetailsQuery(GetClientId(user), orderId);
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static Guid GetClientId(ClaimsPrincipal user)
    {
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Временно для тестов, пока не настроен JWT:
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        return clientId;
    }

    private static IResult ToHttpResponse<T>(Result<T> result)
    {
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }
}