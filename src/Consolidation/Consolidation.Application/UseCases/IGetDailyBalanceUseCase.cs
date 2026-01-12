using Shared.Contracts.UseCase;

namespace Consolidation.Application.UseCases;

/// <summary>
/// Use case boundary for retrieving a daily consolidated balance.
/// </summary>
public interface IGetDailyBalanceUseCase : IUseCaseHandler<GetDailyBalanceQuery, GetDailyBalanceResult?>
{
    
}