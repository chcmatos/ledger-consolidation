using Consolidation.Infrastructure.EventHandlers;
using Consolidation.Application.Ports;
using FluentAssertions;
using Shared.Contracts.Events;
using Xunit;

namespace Consolidation.UnitTests.Application;

/// <summary>
/// Unit tests for applying integration events into the read model.
/// </summary>
public sealed class ApplyTransactionPostedHandlerTests
{
    [Fact]
    public async Task AppliesDebit()
    {
        var repo = new FakeProjectionRepo();
        var uow = new FakeUow();
        var handler = new ApplyTransactionPostedHandler(repo, uow);

        var evt = new TransactionPostedV1(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow), 10m, "Debit", DateTimeOffset.UtcNow);
        await handler.HandleAsync(evt, CancellationToken.None);

        repo.LastDebit.Should().Be(10m);
        repo.LastCredit.Should().Be(0m);
    }

     [Fact]
    public async Task AppliesCredit()
    {
        var repo = new FakeProjectionRepo();
        var uow = new FakeUow();
        var handler = new ApplyTransactionPostedHandler(repo, uow);

        var evt = new TransactionPostedV1(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow), 10m, "Credit", DateTimeOffset.UtcNow);
        await handler.HandleAsync(evt, CancellationToken.None);

        repo.LastDebit.Should().Be(0m);
        repo.LastCredit.Should().Be(10m);
    }

    private sealed class FakeProjectionRepo : IDailyBalanceProjectionRepository
    {
        public decimal LastCredit { get; private set; }
        public decimal LastDebit { get; private set; }

        public Task ApplyCreditAsync(DateOnly date, decimal amount, CancellationToken ct)
        {
            LastCredit = amount;
            return Task.CompletedTask;
        }

        public Task ApplyDebitAsync(DateOnly date, decimal amount, CancellationToken ct)
        {
            LastDebit = amount;
            return Task.CompletedTask;
        }
    }

    private sealed class FakeUow : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
