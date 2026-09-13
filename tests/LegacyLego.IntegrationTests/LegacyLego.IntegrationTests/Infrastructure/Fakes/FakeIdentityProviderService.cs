using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Dto;
using System.Collections.Concurrent;

namespace LegacyLego.IntegrationTests.Infrastructure.Fakes;

public class FakeIdentityProviderService : IIdentityProviderService
{
    private readonly ConcurrentDictionary<Guid, ExternalUserProfile> _profiles = new();

    private readonly ConcurrentDictionary<Guid, int> _callCounts = new();

    private readonly ConcurrentDictionary<Guid, Exception> _errors = new();

    // Метод для тестов: закидываем нужные данные по userId
    public void SetupProfile(ExternalUserProfile profile)
    {
        _profiles[profile.UserId] = profile;
    }

    public Task<ExternalUserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _callCounts.AddOrUpdate(userId, 1, (_, count) => count + 1);

        if (!_profiles.TryGetValue(userId, out var profile))
            if (_errors.TryGetValue(userId, out var error))
                throw error;

        return Task.FromResult(profile);
    }

    public bool WasCalledFor(Guid userId) => _callCounts.ContainsKey(userId);

    public int GetCallCountFor(Guid userId) =>
        _callCounts.TryGetValue(userId, out var count) ? count : 0;

    public void SimulateErrorFor(Guid userId, Exception exception) => _errors[userId] = exception;
}