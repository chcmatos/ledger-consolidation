namespace Ledger.Domain.Rules;

/// <summary>
/// Placeholder for business rules that may evolve (limits, validation, etc.).
/// </summary>
public static class TransactionBusinessRules
{
    public static bool IsValidDescription(string? description) =>
        description is null || description.Length <= 140;
}
