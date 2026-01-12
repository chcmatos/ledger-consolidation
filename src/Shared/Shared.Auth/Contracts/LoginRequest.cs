using System.ComponentModel.DataAnnotations;

namespace Shared.Auth.Contracts;

/// <summary>
/// Login request for issuing a JWT token.
/// </summary>
public sealed record LoginRequest(
    [Required] string Username,
    [Required] string Password
);
