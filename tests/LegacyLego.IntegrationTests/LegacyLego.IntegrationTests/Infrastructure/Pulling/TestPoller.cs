using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace LegacyLego.IntegrationTests.Infrastructure.Pulling;

public static class TestPoller
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan DebuggerTimeout = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan DefaultPollInterval = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Ожидает выполнения произвольного синхронного условия.
    /// </summary>
    public static Task<bool> WaitForAsync(
        Func<bool> predicate,
        TimeSpan? timeout = null,
        TimeSpan? pollInterval = null,
        CancellationToken cancellationToken = default)
    {
        return WaitForAsync(() => Task.FromResult(predicate()), timeout, pollInterval, cancellationToken);
    }

    /// <summary>
    /// Ожидает выполнения произвольного асинхронного условия.
    /// </summary>
    public static async Task<bool> WaitForAsync(
        Func<Task<bool>> predicate,
        TimeSpan? timeout = null,
        TimeSpan? pollInterval = null,
        CancellationToken cancellationToken = default)
    {
        var effectiveTimeout = timeout ?? (Debugger.IsAttached ? DebuggerTimeout : DefaultTimeout);
        var interval = pollInterval ?? DefaultPollInterval;

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(effectiveTimeout);

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (await predicate())
                    {
                        return true;
                    }
                }
                catch when (!cts.Token.IsCancellationRequested)
                {
                    // Игнорируем временные сбои во время поллинга
                }

                await Task.Delay(interval, cts.Token);
            }
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
            // Таймаут истек, предикат так и не вернул true -> возвращаем false
            return false;
        }

        return false;
    }

    /// <summary>
    /// Ожидает выполнения условия внутри короткоживущего Scoped DbContext.
    /// </summary>
    public static Task<bool> WaitForDbContextAsync<TContext>(
        IServiceProvider serviceProvider,
        Func<TContext, Task<bool>> predicate,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default)
        where TContext : DbContext
    {
        return WaitForAsync(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            return await predicate(dbContext);
        }, timeout, cancellationToken: cancellationToken);
    }
}