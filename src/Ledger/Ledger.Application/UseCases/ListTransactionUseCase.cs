using Ledger.Application.Ports;
using Ledger.Application.UseCases.Queries;
using Ledger.Application.UseCases.Results;

namespace Ledger.Application.UseCases;

/// <summary>
/// Use case for list transactions with or without pagination. <br/>
/// <i>Note: Pagination parameters are optional. 
/// When not provided, all transactions for the specified date are returned,
/// limiting the number of results is recommended to avoid performance issues.</i>
/// </summary>
internal sealed class ListTransactionUseCase(ITransactionRepository repo) : IListTransactionUseCase
{
    public async Task<IEnumerable<TransactionResult>> HandleAsync(ListTransactionQuery query, CancellationToken ct)
    {
        if (query.PageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(query.PageNumber), "Page number must be greater than zero.");
        } 
        else if (query.PageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(query.PageSize), "Page size must be greater than zero.");
        }

        var transactions = await repo.ListAsync(query.Date, new Pagination(query.PageNumber, query.PageSize), ct);
        return transactions.Select(t => new TransactionResult(
            t.Id,
            t.BusinessDate,
            t.Amount.Value,
            t.Type.ToString(),
            t.Description));
    }    
}
