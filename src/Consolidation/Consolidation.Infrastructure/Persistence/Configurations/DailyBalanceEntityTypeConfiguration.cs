using Consolidation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consolidation.Infrastructure.Persistence.Configurations;

public sealed class DailyBalanceEntityTypeConfiguration : IEntityTypeConfiguration<DailyBalance>
{
    public void Configure(EntityTypeBuilder<DailyBalance> b)
    {
        b.ToTable("daily_balances");

        b.HasKey(x => x.BusinessDate);

        b.Property(x => x.BusinessDate)
            .HasColumnName("business_date");

        b.Property(x => x.CreditTotal)
            .HasColumnName("credit_total")
            .HasPrecision(18, 2);

        b.Property(x => x.DebitTotal)
            .HasColumnName("debit_total")
            .HasPrecision(18, 2);

        // Amount is derived, not mapped
        b.Ignore(x => x.Amount);
    }
}
