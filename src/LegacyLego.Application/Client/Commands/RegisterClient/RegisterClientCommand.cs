using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Dto;
using LegacyLego.Application.Orders.Commands.Cancel;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed record RegisterClientCommand(ExternalUserProfile ExternalUserProfile) : ICommand<RegisterClientDetails>;