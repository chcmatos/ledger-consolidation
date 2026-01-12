namespace Ledger.Api.Contracts;

/// <summary>
/// REST input contract for creating a transaction.
/// </summary>
public sealed record CreateTransactionRequest(
    DateOnly BusinessDate,
    decimal Amount,
    string Type,
    string? Description
);
