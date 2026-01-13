using System.Diagnostics.CodeAnalysis;
using Shared.Contracts.Events;

namespace Consolidation.Application.Ports;

public interface IApplyTransactionPostedHandler
{
    Task HandleAsync([NotNull] TransactionPostedV1 evt, [NotNull] CancellationToken ct);
}