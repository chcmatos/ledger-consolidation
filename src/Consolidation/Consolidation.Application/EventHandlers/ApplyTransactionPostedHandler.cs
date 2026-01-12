using Consolidation.Application.Ports;
using Consolidation.Domain.Rules;
using Shared.Contracts.Events;

namespace Consolidation.Application.EventHandlers;

/// <summary>
/// Application handler that applies the TransactionPosted integration event into the read model.
/// </summary>
public sealed class ApplyTransactionPostedHandler(
    IDailyBalanceProjectionRepository projectionRepo,
    IUnitOfWork uow)
{
    public async Task HandleAsync(TransactionPostedV1 evt, CancellationToken ct)
    {
        if (ConsolidationRules.IsCredit(evt.Type))
        {
            await projectionRepo.ApplyCreditAsync(evt.BusinessDate, evt.Amount, ct);
        }
        else if (ConsolidationRules.IsDebit(evt.Type))
        {
            await projectionRepo.ApplyDebitAsync(evt.BusinessDate, evt.Amount, ct);
        }
        else
        {
            throw new ArgumentException($"Unknown transaction type: {evt.Type}");
        }

        await uow.CommitAsync(ct);
    }
}
