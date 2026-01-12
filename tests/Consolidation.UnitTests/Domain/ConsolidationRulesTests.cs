using Consolidation.Domain.Rules;
using FluentAssertions;
using Xunit;

namespace Consolidation.UnitTests.Domain;

/// <summary>
/// Unit tests for consolidation rules (delta calculation).
/// </summary>
public sealed class ConsolidationRulesTests
{
    [Fact]
    public void Debit_Valid()
    {
        ConsolidationRules.IsDebit("Debit").Should().BeTrue();
    }

    [Fact]
    public void Credit_Valid()
    {
        ConsolidationRules.IsCredit("Credit").Should().BeTrue();
    }
}
