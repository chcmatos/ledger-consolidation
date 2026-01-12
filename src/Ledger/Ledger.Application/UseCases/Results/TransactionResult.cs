namespace Ledger.Application.UseCases.Results;

public record TransactionResult(
    Guid TransactionId,
    DateOnly BusinessDate,
    decimal Amount,
    string Type,
    string? Description);