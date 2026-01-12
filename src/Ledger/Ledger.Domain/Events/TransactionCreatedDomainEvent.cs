namespace Ledger.Domain.Events;

/// <summary>
/// Domain event raised when a transaction is created (before integration mapping).
/// </summary>
public sealed record TransactionCreatedDomainEvent(Guid TransactionId, DateOnly BusinessDate);
