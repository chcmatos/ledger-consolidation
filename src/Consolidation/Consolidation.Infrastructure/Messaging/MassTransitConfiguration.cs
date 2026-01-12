using Consolidation.Infrastructure.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consolidation.Infrastructure.Messaging;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddConsolidationMessaging(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<TransactionPostedConsumer>();

            // Inbox storage is handled by MassTransit EF Core state; this scaffold keeps it minimal.
            x.AddEntityFrameworkOutbox<Consolidation.Infrastructure.Persistence.ConsolidationDbContext>(o =>
            {
                o.UsePostgres();
            });

            x.UsingRabbitMq((context, rabbit) =>
            {
                rabbit.Host(cfg["RabbitMq:Host"] ?? throw new InvalidOperationException("RabbitMq:Host configuration is missing."), h =>
                {
                    h.Username(cfg["RabbitMq:Username"] ?? throw new InvalidOperationException("RabbitMq:Username configuration is missing."));
                    h.Password(cfg["RabbitMq:Password"] ?? throw new InvalidOperationException("RabbitMq:Password configuration is missing."));
                });

                rabbit.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
