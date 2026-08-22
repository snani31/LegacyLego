using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;
using LegacyLego.Application.Dto;
using LegacyLego.Application.Exceptions;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Services;

public sealed class ClientProvisioningService(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork) : IClientProvisioningService
{
    public async Task<Result<ClientProvisioningResult>> EnsureExistsAsync(
        ExternalUserProfile profile,
        CancellationToken ct = default)
    {
        var clientIdResult = ClientId.From(profile.UserId);
        if (clientIdResult.IsFailure)
            return Result<ClientProvisioningResult>.Failure(clientIdResult.Error);

        var clientId = clientIdResult.Value;

        // Клиент уже существует
        var existingClient = await clientRepository.GetByIdAsync(clientId, ct);
        if (existingClient != null)
            return Result<ClientProvisioningResult>.Success(
                new ClientProvisioningResult(existingClient, ClientProvisioningStatus.AlreadyExists));

        #region Client creation
        var emailResult = Email.Create(profile.Email);
        if (emailResult.IsFailure)
            return Result<ClientProvisioningResult>.Failure(emailResult.Error);

        PhoneNumber? phoneNumber = null;
        if (profile.PhoneNumber is not null)
        {
            var phoneResult = PhoneNumber.Create(profile.PhoneNumber);
            if (phoneResult.IsFailure)
                return Result<ClientProvisioningResult>.Failure(phoneResult.Error);
            phoneNumber = phoneResult.Value;
        }

        var clientResult = Client.Create(
            id: clientId,
            username: profile.Username,
            createdAt: profile.CreatedAtUtc,
            email: emailResult.Value,
            preferences: ClientPreferences.Default,
            firstName: profile.FirstName,
            lastName: profile.LastName,
            phoneNumber: phoneNumber);

        if (clientResult.IsFailure)
            return Result<ClientProvisioningResult>.Failure(clientResult.Error);

        var newClient = clientResult.Value; 
        #endregion
        clientRepository.Add(newClient);

        try
        {
            await unitOfWork.SaveChangesAsync(ct);
            return Result<ClientProvisioningResult>.Success(
                new ClientProvisioningResult(newClient, ClientProvisioningStatus.Created));
        }
        catch (UniqueConstraintViolation) // race condition
        {
            var clientAfterRace = await clientRepository.GetByIdAsync(clientId, ct);
            if (clientAfterRace != null)
            {
                return Result<ClientProvisioningResult>.Success(
                    new ClientProvisioningResult(clientAfterRace, ClientProvisioningStatus.AlreadyExists));
            }

            throw;
        }
    }
}