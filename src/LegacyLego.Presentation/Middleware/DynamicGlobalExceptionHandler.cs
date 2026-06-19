using LegacyLego.Application.Abstractions.ExceptionHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LegacyLego.Presentation.Middleware;

public sealed class DynamicGlobalExceptionHandler : IExceptionHandler
{
    private readonly IEnumerable<IExceptionMapper> _mappers;
    private readonly ILogger<DynamicGlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IProblemDetailsService _problemDetailsService;

    public DynamicGlobalExceptionHandler(
        IEnumerable<IExceptionMapper> mappers,
        ILogger<DynamicGlobalExceptionHandler> logger,
        IWebHostEnvironment env,
        IProblemDetailsService problemDetailsService)
    {
        _mappers = mappers;
        _logger = logger;
        _env = env;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        AppFailureDescription? failureDescription = null;
        foreach (var mapper in _mappers)
        {
            if (mapper.TryMap(exception, out failureDescription) && failureDescription is not null)
                break;
        }

        failureDescription ??= new AppFailureDescription(
            ExceptionFailureKind.Unknown,
            "Внутренняя ошибка сервера",
            "Произошла непредвиденная ошибка в работе системы.");

        LogException(exception, failureDescription, httpContext);

        return await WriteProblemDetailsAsync(httpContext, failureDescription, cancellationToken);
    }

    private void LogException(Exception exception, AppFailureDescription failure, HttpContext context)
    {
        var logLevel = failure.Kind switch
        {
            ExceptionFailureKind.DomainLevelException => LogLevel.Critical,
            ExceptionFailureKind.InfrastructureLevelException => LogLevel.Error,
            _ => LogLevel.Error
        };

        _logger.Log(
            logLevel,
            exception,
            "Перехвачено исключение [{ErrorCode}]: {Title}. Маршрут: {Method} {Path}",
            failure.ErrorCode ?? "Core.Unknown",
            failure.Title,
            context.Request.Method,
            context.Request.Path);
    }

    private async ValueTask<bool> WriteProblemDetailsAsync(HttpContext context, AppFailureDescription failure, CancellationToken ct)
    {
        var statusCode = failure.Kind switch
        {
            ExceptionFailureKind.DomainLevelException => StatusCodes.Status500InternalServerError,
            ExceptionFailureKind.InfrastructureLevelException => StatusCodes.Status503ServiceUnavailable,
            ExceptionFailureKind.UnhandledNetworkLevelException => StatusCodes.Status502BadGateway,
            _ => StatusCodes.Status500InternalServerError
        };

        
        string publicDetail;

        if (_env.IsDevelopment())
        {
            publicDetail = failure.Detail;
        }
        else
        {
            publicDetail = failure.Kind switch
            {
                ExceptionFailureKind.InfrastructureLevelException => "Сервис временно недоступен. Пожалуйста, повторите попытку позже.",
                _ => $"Внутренняя ошибка сервера. Пжалуйста сообщите данный код техподдержке в случае обращения за помощью: {context.TraceIdentifier}" // TraceIdentifier пользователю, чтобы тот сообщил поддержке 
            };
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = _env.IsDevelopment() ? failure.Title : "Внутренняя ошибка сервера",
            Detail = publicDetail,
            Instance = context.Request.Path
        };

        if (_env.IsDevelopment() && !string.IsNullOrEmpty(failure.ErrorCode))
        {
            problemDetails.Extensions["errorCode"] = failure.ErrorCode;
        }

        var problemContext = new ProblemDetailsContext
        {
            HttpContext = context,
            ProblemDetails = problemDetails
        };

        return await _problemDetailsService.TryWriteAsync(problemContext);
    }
}