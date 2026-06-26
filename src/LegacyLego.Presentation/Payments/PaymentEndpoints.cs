using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Errors;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Commands.StartPayment;
using LegacyLego.Presentation.Mock.Common.Dto.Request;
using LegacyLego.Presentation.Payments.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LegacyLego.Presentation.Payments;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var mockGroup = app.MapGroup("/mock")
            .WithDisplayName("Payment")
            .WithDescription("Тестировочные эндпоинты оплаты заказа")
            .WithTags("Payments");

        mockGroup.MapPost("/api/webhooks/payment", HandleWebhook);
        mockGroup.MapPost("/{orderId:guid}/pay", StartPayment);

        return app;
    }

    private static async Task<Results<
        Ok<ProcessPaymentDetails>,
        BadRequest<ProblemDetails>,
        Conflict<ProblemDetails>>> HandleWebhook(
            [FromBody] PaymentProviderWebhookRequest request,
            ICommandDispatcher commandDispatcher,
            CancellationToken ct)
    {
        ProcessPaymentWebhookCommand command;
        try
        {
            command = PaymentWebhookMapper.MapToPaymentWebhookCommand(request);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Request Status",
                Detail = ex.Message
            });
        }

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                ProcessPaymentErrors.InvalidAmountCode or
                ProcessPaymentErrors.UnknownStatusCode =>
                    TypedResults.BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                ProcessPaymentErrors.TransactionConflictCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                ProcessPaymentErrors.TotalPricesMismatchCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                _ => TypedResults.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = error.Code,
                    Detail = error.Message
                })
            };
        }

        return TypedResults.Ok(result.Value);
    }

    private static async Task<Results<
    Ok<StartPaymentResponse>,
    ForbidHttpResult,
    Conflict<ProblemDetails>,
    NotFound<ProblemDetails>,
    BadRequest<ProblemDetails>>> StartPayment(
        [FromRoute] Guid orderId,
        ICommandDispatcher commandDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        // ⌚ Временно для тестов, пока не настроен JWT:
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            clientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        var command = new StartOrderPaymentCommand(OrderId: orderId, ClientId: clientId);

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                OrderApplicationErrors.UnauthorizedAccessToOrderByClientIdCode =>
                    TypedResults.Forbid(),

                StartOrderPaymentErrors.OrderIsNotInPendingPaymentCode or
                StartOrderPaymentErrors.ForOrderIsAlreadyExistsSuccessedPaymentCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                StartOrderPaymentErrors.CanNotFindPendingPaymentAfterCheckConstraintCode =>
                    TypedResults.NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                _ => TypedResults.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = error.Code,
                    Detail = error.Message
                })
            };
        }

        var details = result.Value;

        var response = new StartPaymentResponse(
            CheckoutUrl: details.Session.CheckoutUrl,
            ExpiresAtUtc: details.Session.ExpiresAtUtc
        );

        return TypedResults.Ok(response);
    }
}