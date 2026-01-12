using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.Infrastructure.Messaging;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddLedgerMessaging(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            // EF Core Outbox (transactional outbox)
            x.AddEntityFrameworkOutbox<Ledger.Infrastructure.Persistence.LedgerDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });

            x.UsingRabbitMq((context, rabbit) =>
            {
                rabbit.Host(cfg["RabbitMq:Host"] ?? throw new InvalidOperationException("RabbitMq:Host configuration is missing"), h =>
                {
                    h.Username(cfg["RabbitMq:Username"] ?? throw new InvalidOperationException("RabbitMq:Username configuration is missing"));
                    h.Password(cfg["RabbitMq:Password"] ?? throw new InvalidOperationException("RabbitMq:Password configuration is missing"));
                });

                rabbit.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
