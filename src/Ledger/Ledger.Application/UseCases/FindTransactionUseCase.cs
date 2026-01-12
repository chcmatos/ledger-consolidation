using Ledger.Application.Ports;
using Ledger.Application.UseCases.Queries;
using Ledger.Application.UseCases.Results;

namespace Ledger.Application.UseCases;

internal sealed class FindTransactionUseCase(ITransactionRepository repo) : IFindTransactionUseCase
{
    public async Task<TransactionResult?> HandleAsync(FindTransactionQuery request, CancellationToken cancellationToken)
    {
        var transaction = await repo.FindByIdAsync(request.TransactionId, cancellationToken);
        if (transaction is null)
        {
            return null;
        }

        return new TransactionResult(transaction.Id, 
            transaction.BusinessDate, 
            transaction.Amount.Value,
            transaction.Type.ToString(), 
            transaction.Description);
    }
}