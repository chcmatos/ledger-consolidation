namespace Consolidation.Application.Ports;

/// <summary>
/// Write-side port for updating the daily balance projection.
/// </summary>
public interface IDailyBalanceProjectionRepository
{
    /// <summary>
    /// Applies a credit amount to the daily balance for the specified date.
    /// </summary>
    /// <param name="date">The business date for which the credit is applied.</param>
    /// <param name="amount">The credit amount to apply.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns></returns>
    Task ApplyCreditAsync(DateOnly date, decimal amount, CancellationToken ct);
    
    /// <summary>
    /// Applies a debit amount to the daily balance for the specified date.
    /// </summary>
    /// <param name="date">The business date for which the debit is applied.</param>
    /// <param name="amount">The debit amount to apply.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns></returns>
    Task ApplyDebitAsync(DateOnly date, decimal amount, CancellationToken ct);
}
