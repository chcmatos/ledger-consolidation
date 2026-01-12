namespace Shared.Auth.Security;

/// <summary>
/// JWT settings loaded from configuration.
/// </summary>
public sealed class JwtOptions
{
    public string Issuer { get; init; } = "ledger-consolidation";
    public string Audience { get; init; } = "ledger-consolidation";
    public string SigningKey { get; init; } = string.Empty;
    public int ExpirationMinutes { get; init; } = 15;

    public List<JwtUser> Users { get; init; } = [];
}

/// <summary>
/// Simple dev user for token issuance (demo purposes).
/// </summary>
public sealed class JwtUser
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
