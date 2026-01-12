namespace Consolidation.Domain.ValueObjects;

/// <summary>
/// Value object to represent the business date used for daily balances.
/// </summary>
public readonly record struct BalanceDate(DateOnly Value);
