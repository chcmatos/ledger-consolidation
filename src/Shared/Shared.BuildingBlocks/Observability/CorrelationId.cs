namespace Shared.BuildingBlocks.Observability;

/// <summary>
/// Correlation identifier propagated across boundaries for tracing.
/// </summary>
public static class CorrelationId
{
    public const string HeaderName = "X-Correlation-Id";
}
