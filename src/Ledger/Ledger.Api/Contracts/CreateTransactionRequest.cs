using System.ComponentModel.DataAnnotations;

namespace Ledger.Api.Contracts;

/// <summary>
/// REST input contract for creating a transaction.
/// </summary>
public sealed record CreateTransactionRequest(
    [Required(ErrorMessage = "Business date is required.")]
    DateOnly BusinessDate,

    [Range(0.01, 999999999999.99, ErrorMessage = "Amount must be greater than zero.")]
    decimal Amount,

    [Required(ErrorMessage = "Type is required.")]
    [RegularExpression("(?i)^(credit|debit)$", ErrorMessage = "Type must be 'Credit' or 'Debit'.")]
    string Type,

    [MaxLength(140, ErrorMessage = "Description must be at most 140 characters.")]
    string? Description
);