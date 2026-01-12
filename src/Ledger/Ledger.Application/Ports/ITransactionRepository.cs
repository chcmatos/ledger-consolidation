using Ledger.Domain.Entities;

namespace Ledger.Application.Ports;

/// <summary>
/// Persistence port for ledger transactions.
/// </summary>
public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken ct);
    Task<Transaction?> FindByIdAsync(Guid transactionId, CancellationToken ct);
    Task<IReadOnlyList<Transaction>> ListAsync(DateOnly date, Pagination? pagination = null, CancellationToken ct = default);
}

public record Pagination(int PageNumber, int PageSize);