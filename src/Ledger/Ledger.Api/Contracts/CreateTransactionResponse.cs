namespace Ledger.Api.Contracts;

/// <summary>
/// REST output contract returned after creating a transaction.
/// </summary>
public sealed record CreateTransactionResponse(Guid TransactionId);
