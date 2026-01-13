using Ledger.Application.Ports;
using Ledger.Domain.Entities;
using Ledger.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Infrastructure.Repositories;

internal sealed class TransactionRepository(LedgerDbContext db) : ITransactionRepository
{
    public async Task AddAsync(Transaction transaction, CancellationToken ct)
    {
        await db.Transactions.AddAsync(transaction, ct);
    }

    public async Task<Transaction?> FindByIdAsync(Guid transactionId, CancellationToken ct)
    {
        return await db.Transactions.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == transactionId, ct);
    }

    public async Task<IReadOnlyList<Transaction>> ListAsync(DateOnly date, Pagination? pagination = null, CancellationToken ct = default)
    {
        if (pagination is not null)
        {
            return await db.Transactions.AsNoTracking()
                .Where(x => x.BusinessDate == date)
                .OrderByDescending(x => x.BusinessDate)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(ct);
        }
        else
        {
            return await db.Transactions.AsNoTracking()
                .Where(x => x.BusinessDate == date)
                .OrderByDescending(x => x.BusinessDate)
                .ToListAsync(ct);
        }
    }
}
