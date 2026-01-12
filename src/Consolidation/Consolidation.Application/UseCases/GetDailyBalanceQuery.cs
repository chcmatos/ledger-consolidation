namespace Consolidation.Application.UseCases;

/// <summary>
/// Query boundary for retrieving a daily consolidated balance.
/// </summary>
public sealed record GetDailyBalanceQuery(DateOnly BusinessDate);
