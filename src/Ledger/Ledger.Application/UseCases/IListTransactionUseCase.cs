using Ledger.Application.UseCases.Queries;
using Ledger.Application.UseCases.Results;
using Shared.Contracts.UseCase;

namespace Ledger.Application.UseCases;

/// <summary>
/// Use case for list transactions by business date.
/// </summary>
public interface IListTransactionUseCase : IUseCaseHandler<ListTransactionQuery, IEnumerable<TransactionResult>>
{
    
}