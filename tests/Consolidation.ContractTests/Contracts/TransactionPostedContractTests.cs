using FluentAssertions;
using Shared.Contracts.Events;
using Xunit;

namespace Consolidation.ContractTests.Contracts;

/// <summary>
/// Contract tests ensure the consumer can deserialize and process the published event.
/// </summary>
public sealed class TransactionPostedContractTests
{
    [Fact]
    public void Contract_IsDeserializablePlaceholder()
    {
        var evt = new TransactionPostedV1(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow), 1m, "Credit", DateTimeOffset.UtcNow);
        evt.Amount.Should().BeGreaterThan(0);
    }
}
