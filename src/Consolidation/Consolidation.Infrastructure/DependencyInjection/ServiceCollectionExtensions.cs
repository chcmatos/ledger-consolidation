using Consolidation.Application.EventHandlers;
using Consolidation.Application.Ports;
using Consolidation.Infrastructure.Auth;
using Consolidation.Infrastructure.Messaging;
using Consolidation.Infrastructure.Persistence;
using Consolidation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consolidation.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsolidationInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<ConsolidationDbContext>(o =>
            o.UseNpgsql(cfg.GetConnectionString("ConsolidationDb")));

        services.AddScoped<IDailyBalanceReadRepository, DailyBalanceReadRepository>();
        services.AddScoped<IDailyBalanceProjectionRepository, DailyBalanceProjectionRepository>();
        services.AddScoped<IUnitOfWork>(sp => new EfUnitOfWork(sp.GetRequiredService<ConsolidationDbContext>()));
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ApplyTransactionPostedHandler>();

        services.AddConsolidationMessaging(cfg);

        return services;
    }

    private sealed class EfUnitOfWork(ConsolidationDbContext db) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
    }
}
