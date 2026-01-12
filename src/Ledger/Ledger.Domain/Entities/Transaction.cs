using Ledger.Domain.ValueObjects;

namespace Ledger.Domain.Entities;

/// <summary>
/// Ledger transaction (source of truth) representing a debit or credit entry.
/// </summary>
public sealed class Transaction
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateOnly BusinessDate { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public string? Description { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    private Transaction() { }

    public Transaction(DateOnly businessDate, Money amount, TransactionType type, string? description)
    {
        BusinessDate = businessDate;
        Amount = amount;
        Type = type;
        Description = description;
    }
}
