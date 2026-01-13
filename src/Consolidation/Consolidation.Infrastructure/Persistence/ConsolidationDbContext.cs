using Consolidation.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Consolidation.Infrastructure.Persistence;

internal sealed class ConsolidationDbContext(DbContextOptions<ConsolidationDbContext> options) : DbContext(options)
{
    public DbSet<DailyBalance> DailyBalances => Set<DailyBalance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsolidationDbContext).Assembly);
        // MassTransit EF Inbox/Outbox state tables (Inbox is the important one here)
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        base.OnModelCreating(modelBuilder);
    }
}
