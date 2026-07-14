using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LegacyLego.Infrastructure.Logging.Decoretors;

public sealed class LoggingCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> _inner,
    ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var commandName = typeof(TCommand).Name;

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["CommandType"] = commandName,
            ["CommandData"] = command!
        });

        var stopwatch = Stopwatch.StartNew();
        var result = await _inner.HandleAsync(command, cancellationToken);
        stopwatch.Stop();

        if (result.IsSuccess)
        {
            _logger.LogInformation(
                "Команда {CommandName} успешно выполнена за {ElapsedMs} мс.",
                commandName,
                stopwatch.ElapsedMilliseconds);
        }
        else
        {
            _logger.LogError(
                "Ошибка выполнения команды {CommandName} ({ElapsedMs} мс). Код ошибки: {ErrorCode}. Причина: {ErrorMessage}",
                commandName,
                stopwatch.ElapsedMilliseconds,
                result.Error.Code,
                result.Error.Message);
        }

        return result;
    }
}

public sealed class LoggingCommandHandlerDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> _inner,
    ILogger<LoggingCommandHandlerDecorator<TCommand, TResponse>> _logger)
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var commandName = typeof(TCommand).Name;

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["CommandType"] = commandName,
            ["CommandData"] = command!
        });

        var stopwatch = Stopwatch.StartNew();
        var result = await _inner.HandleAsync(command, cancellationToken);
        stopwatch.Stop();

        if (result.IsSuccess)
        {
            var isWarning = result.Value is ICustomLogSeverity customLog && customLog.IsWarning;

            if (isWarning)
            {
                _logger.LogWarning(
                    "Команда {CommandName} выполнена с предупреждением за {ElapsedMs} мс. Результат: {@Details}",
                    commandName,
                    stopwatch.ElapsedMilliseconds,
                    result.Value);
            }
            else
            {
                _logger.LogInformation(
                    "Команда {CommandName} успешно выполнена за {ElapsedMs} мс. Результат: {@Details}",
                    commandName,
                    stopwatch.ElapsedMilliseconds,
                    result.Value);
            }
        }
        else
        {
            _logger.LogError(
                "Ошибка выполнения команды {CommandName} ({ElapsedMs} мс). Код ошибки: {ErrorCode}. Причина: {ErrorMessage}",
                commandName,
                stopwatch.ElapsedMilliseconds,
                result.Error.Code,
                result.Error.Message);
        }

        return result;
    }
}