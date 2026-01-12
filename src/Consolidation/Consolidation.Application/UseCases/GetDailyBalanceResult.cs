namespace Consolidation.Application.UseCases;

/// <summary>
/// Query result returned to the API.
/// </summary>
public sealed record GetDailyBalanceResult(
    DateOnly BusinessDate, 
    decimal CreditTotal,
    decimal DebitTotal,
    decimal Amount);