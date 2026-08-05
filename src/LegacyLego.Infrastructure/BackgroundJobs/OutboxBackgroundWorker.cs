using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Converters.JsonConverters;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public sealed class OutboxBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxBackgroundWorker> _logger;

    private readonly IOptionsMonitor<OutboxBackgroundWorkerOptions> _optionsMonitor;

    private TimeSpan Period
    {
        get => TimeSpan.FromSeconds(_optionsMonitor.CurrentValue.SecondsPeriod);
    }

    private int TakeRecordsNum
    {
        get => _optionsMonitor.CurrentValue.TakeRecordsNum;
    }

    public OutboxBackgroundWorker(
        IServiceProvider serviceProvider,
        ILogger<OutboxBackgroundWorker> logger,
        IOptionsMonitor<OutboxBackgroundWorkerOptions> optionsMonitor)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _optionsMonitor = optionsMonitor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Outbox Background Worker успешно запущен.");

        using var timer = new PeriodicTimer(Period);

        while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessOutboxMessagesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Критическая ошибка при выполнении фоновой задачи Outbox.");
            }
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken ct)
    {
        List<OutboxMessage> messages;
        using (var readScope = _serviceProvider.CreateScope())
        {
            var context = readScope.ServiceProvider.GetRequiredService<OrderContext>();
            messages = context.Set<OutboxMessage>()
                .TagWith("OutboxPolling")
                .Where(m => m.ProcessedOnUtc == null)
                .OrderBy(m => m.OccurredOnUtc)
                .Take(TakeRecordsNum)
                .ToList();
        }

        if (messages.Count == 0) return;

        _logger.LogInformation("Найдено {Count} необработанных сообщений в Outbox.", messages.Count);

        foreach (var message in messages)
        {
            using var actionScope = _serviceProvider.CreateScope();
            var context = actionScope.ServiceProvider.GetRequiredService<OrderContext>();
            var domainEventDispatcher = actionScope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
            var timeProvider = actionScope.ServiceProvider.GetRequiredService<TimeProvider>();

            context.Attach(message);

            try
            {
                // 1. Восстанавливаем .NET тип из строки AssemblyQualifiedName
                Type? eventType = Type.GetType(message.Type);
                if (eventType == null) 
                {
                    _logger.LogError("Не удалось восстановить тип события: {Type}", message.Type);
                    message.Error = $"Тип .NET '{message.Type}' не найден в сборках.";
                    message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                    continue;
                }

                // 2. Десериализуем JSON контент обратно в объект доменного события
                var domainEvent = JsonSerializer.Deserialize(message.Content, eventType, OutboxSerializerOptions.Options);
                if (domainEvent is not IDomainEvent validEvent) 
                {
                    _logger.LogError("Объект сообщения не реализует IDomainEvent");
                    message.Error = "Объект десериализации не является IDomainEvent.";
                    message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                    continue;
                }

                await domainEventDispatcher.DispatchAsync(validEvent, ct);
                // в случае успеха маркируем дату успешной обработки
                message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                message.Error = null; // очистить ошибки, если были ранее
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке Outbox сообщения с Id: {Id}", message.Id);
                message.Error = ex.ToString();
            }

            await context.SaveChangesAsync(ct);
        }
    }
}