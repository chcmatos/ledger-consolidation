namespace Consolidation.Domain.Entities;

/// <summary>
/// Read model entity for consolidated daily balance.
/// Stores credit and debit totals separately; net amount is derived.
/// </summary>
public sealed class DailyBalance
{
    public DateOnly BusinessDate { get; private set; }
    
    public decimal CreditTotal { get; private set; }
    
    public decimal DebitTotal { get; private set; }
    
    /// <summary>
    /// Net amount = CreditTotal - DebitTotal.
    /// This is a derived value.
    /// </summary>
    public decimal Amount => CreditTotal - DebitTotal;

    private DailyBalance() { }

    public DailyBalance(DateOnly businessDate)
    {
        BusinessDate = businessDate;
        CreditTotal = 0m;
        DebitTotal = 0m;
    }

    public void ApplyCredit(decimal amount)
    {
        if (amount <= 0) return;
        CreditTotal += amount;
    }

    public void ApplyDebit(decimal amount)
    {
        if (amount <= 0) return;
        DebitTotal += amount;
    }
}
