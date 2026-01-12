namespace Shared.Auth.Contracts;

/// <summary>
/// JWT token response.
/// </summary>
public sealed record LoginResponse(string AccessToken, int ExpiresInSeconds);
