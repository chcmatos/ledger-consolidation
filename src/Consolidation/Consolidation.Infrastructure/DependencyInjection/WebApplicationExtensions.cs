namespace Consolidation.Infrastructure.DependencyInjection;

using Consolidation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.BuildingBlocks.Resilience;

public static class WebApplicationExtensions
{
    public static WebApplication ApplyDatabaseMigration(this WebApplication app)
    {
        Retry.With(
            action: () =>
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ConsolidationDbContext>();
                db.Database.Migrate();
            },
            maxAttempts: 15,
            delay: TimeSpan.FromSeconds(2),
            onRetry: (attempt, ex) =>
            {
                app.Logger.LogWarning(ex, "Consolidation DB not ready. Retry {Attempt}/15.", attempt);
            });
        return app;
    }
}