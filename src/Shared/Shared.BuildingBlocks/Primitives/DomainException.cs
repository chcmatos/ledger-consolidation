namespace Shared.BuildingBlocks.Primitives;

/// <summary>
/// Exception reserved for invariant violations inside the domain layer.
/// </summary>
public abstract class DomainException(string message) : Exception(message);
