using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Orders.Errors;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed class StartOrderPaymentCommandHandler(
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPaymentProvider paymentProvider,
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<StartOrderPaymentCommand, StartOrderPaymentDetails>
{
    private enum ConstraintCheckTimeline : byte
    {
        AfterConstraintCheck,
        BeforeConstraintCheck
    }

    public async Task<Result<StartOrderPaymentDetails>> HandleAsync(StartOrderPaymentCommand command, CancellationToken ct)
    {
        var orderId = OrderId.From(command.OrderId);
        var clientId = command.ClientId;

        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<StartOrderPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        if (order.ClientId != command.ClientId)
            return Result<StartOrderPaymentDetails>.Failure(OrderApplicationErrors.GetUnauthorizedAccessToOrderByClientIdError(orderId, clientId));

        if (order.Status != OrderStatus.PendingPayment)
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetOrderIsNotInPendingPaymentError(command.OrderId, order.Status));

        if (await paymentRepository.ExistsSucceededAsync(orderId, ct))
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetForOrderIsAlreadyExistsSuccessedPaymentError(command.OrderId));

        var existingBeforeCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

        if (existingBeforeCheckUniqConstraint is not null)
        {
            return await EnsureSession(
                existingBeforeCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                timeProvider,
                ConstraintCheckTimeline.BeforeConstraintCheck,
                ct);
        }

        var now = timeProvider.GetUtcNow().UtcDateTime;
        var paymentResult = OrderPayment.Create(orderId, order.TotalPrice, now);

        if(paymentResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(paymentResult.Error);

        var payment = paymentResult.Value;

        paymentRepository.Add(payment);

        try
        {
            await unitOfWork.SaveChangesAsync(ct);
        }
        catch (UniqueConstraintViolation)
        {
            var existingAfterCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

            if (existingAfterCheckUniqConstraint is null)
                return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetCanNotFindPendingPaymentAfterCheckConstraintError(command.OrderId));

            return await EnsureSession(
                existingAfterCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                timeProvider,
                ConstraintCheckTimeline.AfterConstraintCheck,
                ct);
        }

        var sessionResult = await paymentProvider.CreatePaymentSessionAsync(
                    paymentId: payment.Id.Value,
                    orderId: order.Id.Value,
                    amount: order.TotalPrice.Sum,
                    currency: order.TotalPrice.Currency.Code,
                    scale: order.Currency.Scale,
                    ct: ct);

        if (sessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(sessionResult.Error);

        var session = sessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);
        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);

        payment.AttachSession(extrernalSessionResult.Value, timeProvider.GetUtcNow().UtcDateTime);

        await unitOfWork.SaveChangesAsync(ct);

        return Result<StartOrderPaymentDetails>.Success(
            StartOrderPaymentDetails.GetNewPaymentWithNewSessionDetails(session, orderId.Value));
    }

    private static async Task<Result<StartOrderPaymentDetails>> EnsureSession(
        OrderPayment payment,
        Order order,
        IPaymentProvider paymentProvider,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        ConstraintCheckTimeline timeline,
        CancellationToken ct = default)
    {
        PaymentSession session;

        if (payment.HasSession && !payment.ExternalSession!.IsExpired(timeProvider.GetUtcNow().UtcDateTime))
        {
            session = new PaymentSession(
                payment.Id.Value,
                payment.ExternalSession.ExternalId,
                payment.ExternalSession.CheckoutUrl,
                payment.ExternalSession.ExpiresAtUtc);


            return timeline switch
            {
                ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

                ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(session, order.Id.Value)),

                _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
            };
        }

        var newSessionResult = await paymentProvider.CreatePaymentSessionAsync(
                paymentId: payment.Id.Value,
                orderId: order.Id.Value,
                amount: order.TotalPrice.Sum,
                currency: order.TotalPrice.Currency.Code,
                scale: order.Currency.Scale,
                ct: ct);

        if (newSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(newSessionResult.Error);

        session = newSessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);

        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);


        payment.AttachSession(extrernalSessionResult.Value, timeProvider.GetUtcNow().UtcDateTime);

        await unitOfWork.SaveChangesAsync(ct);

        return timeline switch
        {
            ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

            ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(session, order.Id.Value)),

            _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
        };
    }
}