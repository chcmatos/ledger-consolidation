using Consolidation.Application.Ports;
using MassTransit;
using Shared.Contracts.Events;

namespace Consolidation.Infrastructure.Messaging.Consumers;

internal sealed class TransactionPostedConsumer(IApplyTransactionPostedHandler handler) : IConsumer<TransactionPostedV1>
{
    public Task Consume(ConsumeContext<TransactionPostedV1> context) =>
        handler.HandleAsync(context.Message, context.CancellationToken);
}
