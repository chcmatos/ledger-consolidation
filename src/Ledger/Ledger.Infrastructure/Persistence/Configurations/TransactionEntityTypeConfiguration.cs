using Ledger.Domain.Entities;
using Ledger.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Infrastructure.Persistence.Configurations;

public sealed class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> b)
    {
        b.ToTable("transactions");
        b.HasKey(x => x.Id);

        b.Property(x => x.BusinessDate)
            .HasColumnName("business_date");

        // Map Money as a single decimal column using a ValueConverter
        b.Property(x => x.Amount)
            .HasConversion(
                v => v.Value,          // Money -> decimal
                v => new Money(v)      // decimal -> Money (constructor of record struct)
            )
            .HasColumnName("amount")
            .HasPrecision(18, 2);

        b.Property(x => x.Type)
            .HasColumnName("type");

        b.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(140);

        b.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();
    }
}
