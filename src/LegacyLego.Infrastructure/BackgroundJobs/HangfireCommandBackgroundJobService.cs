using Hangfire;
using Hangfire.States;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Infrastructure.Options;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public sealed class HangfireCommandBackgroundJobService : ICommandBackgroundJobService
{
    private readonly IBackgroundJobClient _jobClient;

    public HangfireCommandBackgroundJobService(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public void Schedule<TResult>(ICommand<TResult> command, TimeSpan delay)
    {
        var methodCall = Hangfire.Common.Job.FromExpression<ICommandDispatcher>(
            dispatcher => dispatcher.DispatchAsync(command, CancellationToken.None), HangfireOptions.CommandHangfireQueueName);

        var state = new ScheduledState(delay);

        _jobClient.Create(methodCall, state);
    }

    public void Schedule(ICommand command, TimeSpan delay)
    {
        var methodCall = Hangfire.Common.Job.FromExpression<ICommandDispatcher>(
            dispatcher => dispatcher.DispatchAsync(command, CancellationToken.None), HangfireOptions.CommandHangfireQueueName);

        var state = new ScheduledState(delay);

        _jobClient.Create(methodCall, state);
    }
}