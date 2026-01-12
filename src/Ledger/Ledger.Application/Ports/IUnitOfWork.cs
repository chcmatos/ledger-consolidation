namespace Ledger.Application.Ports;

/// <summary>
/// Transaction boundary abstraction for atomic operations.
/// </summary>
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
}
