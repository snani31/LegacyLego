namespace LegacyLego.Application.Abstractions.ExceptionHandling;

public record AppFailureDescription(
    ExceptionFailureKind Kind,
    string Title,
    string Detail,
    string? ErrorCode = null);