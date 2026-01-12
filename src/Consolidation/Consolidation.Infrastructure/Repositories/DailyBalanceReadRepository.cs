using Consolidation.Application.Ports;
using Consolidation.Domain.Entities;
using Consolidation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Consolidation.Infrastructure.Repositories;

public sealed class DailyBalanceReadRepository(ConsolidationDbContext db) : IDailyBalanceReadRepository
{
    public Task<DailyBalance?> GetByDateAsync(DateOnly date, CancellationToken ct) =>
        db.DailyBalances.AsNoTracking().FirstOrDefaultAsync(x => x.BusinessDate == date, ct);
}
