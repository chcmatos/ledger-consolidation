namespace Ledger.Application.UseCases.Results;

/// <summary>
/// Use case result with created transaction id.
/// </summary>
public sealed record CreateTransactionResult(Guid TransactionId);
