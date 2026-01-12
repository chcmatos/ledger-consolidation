using FluentAssertions;
using Ledger.Application.Ports;
using Ledger.Application.UseCases;
using Ledger.Application.UseCases.Commands;
using Ledger.Domain.Entities;
using Shared.Contracts.Events;
using Xunit;

namespace Ledger.UnitTests.Application;

/// <summary>
/// Unit tests for the create transaction use case.
/// </summary>
public sealed class CreateTransactionHandlerTests
{
    [Fact]
    public async Task CreatesTransactionAndEnqueuesEvent()
    {
        var repo = new FakeRepo();
        var uow = new FakeUow();
        var published = new List<TransactionPostedV1>();

        var handler = new CreateTransactionUseCase(repo, uow, evt => { published.Add(evt); return Task.CompletedTask; });

        var cmd = new CreateTransactionCommand(DateOnly.FromDateTime(DateTime.UtcNow), 10m, "Credit", "test");
        var res = await handler.HandleAsync(cmd, CancellationToken.None);

        res.TransactionId.Should().NotBe(Guid.Empty);
        published.Should().HaveCount(1);
    }

    private sealed class FakeRepo : ITransactionRepository
    {
        public Task AddAsync(Transaction transaction, CancellationToken ct) => Task.CompletedTask;

        public Task<Transaction?> FindByIdAsync(Guid transactionId, CancellationToken ct)
        {
            return Task.FromResult<Transaction?>(null);
        }

        public Task<IReadOnlyList<Transaction>> ListAsync(CancellationToken ct) => Task.FromResult((IReadOnlyList<Transaction>)Array.Empty<Transaction>());

        public Task<IReadOnlyList<Transaction>> ListAsync(DateOnly date, Pagination? pagination = null, CancellationToken ct = default)
        {
            return Task.FromResult((IReadOnlyList<Transaction>)Array.Empty<Transaction>());
        }
    }

    private sealed class FakeUow : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
