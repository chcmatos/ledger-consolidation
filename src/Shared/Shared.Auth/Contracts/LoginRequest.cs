using System.ComponentModel.DataAnnotations;

namespace Shared.Auth.Contracts;

/// <summary>
/// Login request for issuing a JWT token.
/// </summary>
public sealed record LoginRequest(
    [Required(ErrorMessage = "Username is required")] string Username,
    [Required(ErrorMessage = "Password is required")] string Password
);
