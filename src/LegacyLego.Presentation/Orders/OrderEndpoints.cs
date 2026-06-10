using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Errors;
using LegacyLego.Application.Orders.Commands.Create;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Commands.StartPayment;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LegacyLego.Presentation.Payments.Dto;
using LegacyLego.Presentation.Orders.Dto;
namespace LegacyLego.Presentation.Orders;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder UseOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var ordersGroup = app.MapGroup("/orders")
            .WithDisplayName("Orders")
            .WithDescription("Управление заказами")
            .WithGroupName("Orders");

        ordersGroup.MapPost("", Create);

        return app;
    }

    private static async Task<Results<Created<Guid>, BadRequest<ProblemDetails>>> Create(
        [FromBody] CreateOrderRequest request,
        ICommandDispatcher commandDispatcher,
        ClaimsPrincipal user,
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
            return TypedResults.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = result.Error.Code,
                Detail = result.Error.Message
            });
        }

        return TypedResults.Created($"/orders/{result.Value}", result.Value);
    }
}