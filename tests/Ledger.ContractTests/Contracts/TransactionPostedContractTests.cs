using FluentAssertions;
using Shared.Contracts.Events;
using Xunit;

namespace Ledger.ContractTests.Contracts;

/// <summary>
/// Contract tests protect integration-event compatibility over time.
/// </summary>
public sealed class TransactionPostedContractTests
{
    [Fact]
    public void Contract_HasStableShape()
    {
        var evt = new TransactionPostedV1(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow), 1m, "Credit", DateTimeOffset.UtcNow);
        evt.Type.Should().NotBeNullOrWhiteSpace();
    }
}
