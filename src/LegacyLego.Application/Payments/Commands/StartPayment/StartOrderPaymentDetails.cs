using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record StartOrderPaymentDetails
{
    public const string NewPaymentWithNewSessionCode = "StartOrderPayment.NewPaymentWithNewSession";

    public const string ExistingPaymentWithNewSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionBeforeCheckConstraint";
    public const string ExistingPaymentWithNewSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionAfterCheckConstraint";

    public const string ExistingPaymentWithExistingSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionAfterCheckConstraint";
    public const string ExistingPaymentWithExistingSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionBeforeCheckConstraint";

    public readonly string Code;
    public readonly string Message;
    public readonly Guid OrderId;
    public readonly PaymentSession Session;

    private StartOrderPaymentDetails(string code,
    string message,
    PaymentSession session,
    Guid orderId)
    {
        Code = code;
        OrderId = orderId;
        Message = message;
        Session = session;
    }

    internal static StartOrderPaymentDetails GetNewPaymentWithNewSessionDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: NewPaymentWithNewSessionCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"created new Payment with PaymentId: {session.PaymentId} " +
            $"and session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }
}