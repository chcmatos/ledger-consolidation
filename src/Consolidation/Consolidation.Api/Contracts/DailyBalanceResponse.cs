namespace Consolidation.Api.Contracts;

/// <summary>
/// REST output contract for consolidated daily balance.
/// </summary>
/// <param name="BusinessDate">The business date for the balance.</param>
/// <param name="CreditTotal">The total credit amount for the day.</param>
/// <param name="DebitTotal">The total debit amount for the day.</param>
/// <param name="Amount">The consolidated balance amount.</param>
public sealed record DailyBalanceResponse(
    DateOnly BusinessDate, 
    decimal CreditTotal,
    decimal DebitTotal,
    decimal Amount);
