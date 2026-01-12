using FluentAssertions;
using Ledger.Domain.ValueObjects;
using Xunit;

namespace Ledger.UnitTests.Domain;

/// <summary>
/// Unit tests for domain invariants.
/// </summary>
public sealed class TransactionTests
{
    [Fact]
    public void Money_MustBePositive()
    {
        var act = () => Money.Of(0);
        act.Should().Throw<Exception>();
    }
}
