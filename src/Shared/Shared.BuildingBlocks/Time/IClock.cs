namespace Shared.BuildingBlocks.Time;

/// <summary>
/// Abstraction for time to enable deterministic unit tests.
/// </summary>
public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
