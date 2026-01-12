namespace Ledger.Application.UseCases.Queries;

public sealed record ListTransactionQuery(DateOnly Date, int PageNumber = 1, int PageSize = 500);