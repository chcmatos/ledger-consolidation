using Ledger.Application.UseCases.Queries;
using Ledger.Application.UseCases.Results;
using Shared.Contracts.UseCase;

namespace Ledger.Application.UseCases;

/// <summary>
/// Use case to find a transaction by its ID.
/// </summary>
public interface IFindTransactionUseCase : IUseCaseHandler<FindTransactionQuery, TransactionResult?>
{
    
}