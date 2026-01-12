namespace Shared.Contracts.Events;

/// <summary>
/// Integration event emitted by Ledger when a transaction is committed.<br/>
/// <i>v1</i> indicates the version of this contract.<br/>
/// <i>Note:</i> Use versioning in the namespace to allow for future non-breaking changes.
/// </summary>
public sealed record TransactionPostedV1(
    Guid EventId,
    Guid TransactionId,
    DateOnly BusinessDate,
    decimal Amount,
    string Type,
    DateTimeOffset OccurredAt
);
