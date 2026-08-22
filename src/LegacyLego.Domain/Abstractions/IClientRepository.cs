using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IClientRepository
{
    public Task<Client?> GetByIdAsync(ClientId id, CancellationToken ct = default);
    public Task<bool> ExistsAsync(ClientId id, CancellationToken ct = default);
    public void Add(Client client);
}