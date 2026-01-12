namespace Consolidation.Application.Ports;

/// <summary>
/// Transaction boundary abstraction for atomic projection updates.
/// </summary>
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
}
