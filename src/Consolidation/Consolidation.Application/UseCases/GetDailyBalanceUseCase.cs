using Consolidation.Application.Ports;

namespace Consolidation.Application.UseCases;

internal sealed class GetDailyBalanceUseCase(IDailyBalanceReadRepository repo) : IGetDailyBalanceUseCase
{
    public async Task<GetDailyBalanceResult?> HandleAsync(GetDailyBalanceQuery query, CancellationToken ct)
    {
        var found = await repo.GetByDateAsync(query.BusinessDate, ct);
        return found is null ? null : new GetDailyBalanceResult(
            found.BusinessDate, 
            found.CreditTotal, 
            found.DebitTotal, 
            found.Amount);
    }
}
