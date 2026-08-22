using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace LegacyLego.Infrastructure.Repositories;

internal class ClientRepository(OrderContext context) : IClientRepository
{
    public void Add(Client client) => context.Clients.Add(client);

    public async Task<bool> ExistsAsync(ClientId id, CancellationToken ct = default) =>
        await context.Clients.AnyAsync(c => c.Id == id, ct);

    public async Task<Client?> GetByIdAsync(ClientId id, CancellationToken ct = default) =>
        await context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct);
}
