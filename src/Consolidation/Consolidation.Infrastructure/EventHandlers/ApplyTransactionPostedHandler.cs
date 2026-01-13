using System.Diagnostics.CodeAnalysis;
using Consolidation.Application.Ports;
using Consolidation.Domain.Rules;
using Shared.Contracts.Events;

namespace Consolidation.Infrastructure.EventHandlers;

/// <summary>
/// Application handler that applies the TransactionPosted integration event into the read model.
/// </summary>
internal sealed class ApplyTransactionPostedHandler(
    IDailyBalanceProjectionRepository projectionRepo,
    IUnitOfWork uow) : IApplyTransactionPostedHandler
{
    public async Task HandleAsync([NotNull] TransactionPostedV1 evt, [NotNull] CancellationToken ct)
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
