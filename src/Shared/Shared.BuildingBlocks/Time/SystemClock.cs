namespace Shared.BuildingBlocks.Time;

/// <summary>
/// Production clock based on system time.
/// </summary>
public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
