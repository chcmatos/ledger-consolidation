using Consolidation.Application.Ports;
using Consolidation.Domain.Entities;
using Consolidation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Consolidation.Infrastructure.Repositories;

public sealed class DailyBalanceProjectionRepository(ConsolidationDbContext db) : IDailyBalanceProjectionRepository
{
    public async Task ApplyCreditAsync(DateOnly date, decimal amount, CancellationToken ct)
    {
        var entity = await GetOrCreate(date, ct);
        entity.ApplyCredit(amount);
    }

    public async Task ApplyDebitAsync(DateOnly date, decimal amount, CancellationToken ct)
    {
        var entity = await GetOrCreate(date, ct);
        entity.ApplyDebit(amount);
    }

    private async Task<DailyBalance> GetOrCreate(DateOnly date, CancellationToken ct)
    {
        var entity = await db.DailyBalances.FirstOrDefaultAsync(x => x.BusinessDate == date, ct);
        if (entity is null)
        {
            entity = new DailyBalance(date);
            db.DailyBalances.Add(entity);
        }
        return entity;
    }
}
