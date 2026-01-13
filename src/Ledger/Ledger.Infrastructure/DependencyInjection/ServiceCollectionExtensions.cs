using Ledger.Application.Ports;
using Ledger.Infrastructure.Auth;
using Ledger.Infrastructure.Messaging;
using Ledger.Infrastructure.Persistence;
using Ledger.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Events;

namespace Ledger.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedgerInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<LedgerDbContext>(o =>
            o.UseNpgsql(cfg.GetConnectionString("LedgerDb")));

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUnitOfWork>(sp => new EfUnitOfWork(sp.GetRequiredService<LedgerDbContext>()));
        services.AddScoped<IAuthService, AuthService>();

        services.AddLedgerMessaging(cfg);

        // Delegate used by Application; MassTransit EF Outbox ensures reliable publish after commit.
        services.AddScoped<Func<TransactionPostedV1, Task>>(sp =>
        {
            var publish = sp.GetRequiredService<IPublishEndpoint>();
            return evt => publish.Publish(evt);
        });

        return services;
    }

    private sealed class EfUnitOfWork(LedgerDbContext db) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
    }
}
