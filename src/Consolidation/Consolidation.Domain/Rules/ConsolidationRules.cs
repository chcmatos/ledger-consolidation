using System.ComponentModel.DataAnnotations;
using Shared.BuildingBlocks.Primitives;

namespace Consolidation.Domain.Rules;

/// <summary>
/// Consolidation rules: debit decreases, credit increases.
/// </summary>
public static class ConsolidationRules
{
    public static bool IsCredit(string type) =>
        type.Equals("Credit", StringComparison.OrdinalIgnoreCase);

    public static bool IsDebit(string type) =>
        type.Equals("Debit", StringComparison.OrdinalIgnoreCase);
}
