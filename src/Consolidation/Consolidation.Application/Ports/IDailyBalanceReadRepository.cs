using Consolidation.Domain.Entities;

namespace Consolidation.Application.Ports;

/// <summary>
/// Read-side port for querying daily balances.
/// </summary>
public interface IDailyBalanceReadRepository
{
    Task<DailyBalance?> GetByDateAsync(DateOnly date, CancellationToken ct);
}
