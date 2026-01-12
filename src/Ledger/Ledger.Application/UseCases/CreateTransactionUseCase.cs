using Ledger.Application.Ports;
using Ledger.Domain.Entities;
using Ledger.Domain.ValueObjects;
using Shared.Contracts.Events;
using Ledger.Application.UseCases.Commands;
using Ledger.Application.UseCases.Results;

namespace Ledger.Application.UseCases;

internal sealed class CreateTransactionUseCase(
    ITransactionRepository repo,
    IUnitOfWork uow,
    Func<TransactionPostedV1, Task> publishIntegrationEvent) : ICreateTransactionUseCase
{
    public async Task<CreateTransactionResult> HandleAsync(CreateTransactionCommand cmd, CancellationToken ct)
    {
        var type = Enum.Parse<TransactionType>(cmd.Type, ignoreCase: true);
        var money = Money.Of(cmd.Amount);

        var tx = new Transaction(cmd.BusinessDate, money, type, cmd.Description);
        await repo.AddAsync(tx, ct);

        // Publishing is wired to MassTransit (with EF Outbox) in Infrastructure.
        var evt = new TransactionPostedV1(
            EventId: Guid.NewGuid(),
            TransactionId: tx.Id,
            BusinessDate: tx.BusinessDate,
            Amount: tx.Amount.Value,
            Type: tx.Type.ToString(),
            OccurredAt: DateTimeOffset.UtcNow
        );

        await publishIntegrationEvent(evt);

        await uow.CommitAsync(ct);
        return new CreateTransactionResult(tx.Id);
    }
}
