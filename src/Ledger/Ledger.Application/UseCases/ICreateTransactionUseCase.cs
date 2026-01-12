using Shared.Contracts.UseCase;
using Ledger.Application.UseCases.Commands;
using Ledger.Application.UseCases.Results;

namespace Ledger.Application.UseCases;

/// <summary>
/// Use case to create a new transaction.
/// </summary>
public interface ICreateTransactionUseCase : IUseCaseHandler<CreateTransactionCommand, CreateTransactionResult>
{
    
}
