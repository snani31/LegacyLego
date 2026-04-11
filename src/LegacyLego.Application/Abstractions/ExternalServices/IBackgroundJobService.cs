using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IBackgroundJobService
{
    public void Schedule(IBaseCommand command, TimeSpan delay);
}