using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface ICommandBackgroundJobService
{
    public void Schedule<TResult>(ICommand<TResult> command, TimeSpan delay);

    public void Schedule(ICommand command, TimeSpan delay);
}