namespace Shared.BuildingBlocks.Resilience;

public static class Retry
{
    public const int DefaultMaxAttempts = 10;
    public static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(2);


    /// <summary>
    /// Runs an action with a fixed retry policy. The action can be any IO operation (e.g., EF Migrate).
    /// </summary>
    public static void With(
        Action action,
        int maxAttempts = DefaultMaxAttempts,
        TimeSpan? delay = null,
        Action<int, Exception>? onRetry = null)
    {
        var wait = delay ?? DefaultDelay;
        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                action();
                return;
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                onRetry?.Invoke(attempt, ex);
                Thread.Sleep(wait);
            }
        }

        // last attempt (let it throw if it still fails)
        action();
    }

    /// <summary>
    /// Async variant (useful if you want to await IO).
    /// </summary>
    public static async Task WithAsync(
        Func<Task> action,
        int maxAttempts = DefaultMaxAttempts,
        TimeSpan? delay = null,
        Func<int, Exception, Task>? onRetry = null)
    {
        var wait = delay ?? DefaultDelay;
        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await action();
                return;
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                if (onRetry is not null) await onRetry(attempt, ex);
                await Task.Delay(wait);
            }
        }

        await action();
    }
}
