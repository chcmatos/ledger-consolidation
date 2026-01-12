using Ledger.Domain.Exceptions;
using Shared.BuildingBlocks.Primitives;

namespace Ledger.Domain.ValueObjects;

/// <summary>
/// Value object representing a monetary amount.
/// </summary>
public readonly record struct Money(decimal Value)
{
    public static Money Of(decimal value)
    {
        if (value <= 0) throw new MoneyException($"Amount must be positive: {value}");
        return new Money(value);
    }
}
