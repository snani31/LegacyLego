namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;