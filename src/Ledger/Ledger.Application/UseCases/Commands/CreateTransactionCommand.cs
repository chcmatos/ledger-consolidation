namespace Ledger.Application.UseCases.Commands;

/// <summary>
/// Input contract for creating a transaction (application boundary).
/// </summary>
public sealed record CreateTransactionCommand(
    DateOnly BusinessDate,
    decimal Amount,
    string Type,
    string? Description
);
