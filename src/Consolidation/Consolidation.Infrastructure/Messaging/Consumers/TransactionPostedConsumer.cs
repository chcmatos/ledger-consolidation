using Consolidation.Application.EventHandlers;
using MassTransit;
using Shared.Contracts.Events;

namespace Consolidation.Infrastructure.Messaging.Consumers;

public sealed class TransactionPostedConsumer(ApplyTransactionPostedHandler handler) : IConsumer<TransactionPostedV1>
{
    public Task Consume(ConsumeContext<TransactionPostedV1> context) =>
        handler.HandleAsync(context.Message, context.CancellationToken);
}
