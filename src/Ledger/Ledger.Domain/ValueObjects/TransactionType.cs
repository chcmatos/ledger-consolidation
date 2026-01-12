namespace Ledger.Domain.ValueObjects;

/// <summary>
/// Allowed transaction types. Keep it closed to enforce invariants.
/// </summary>
public enum TransactionType
{
    Debit = 0,
    Credit = 1
}
