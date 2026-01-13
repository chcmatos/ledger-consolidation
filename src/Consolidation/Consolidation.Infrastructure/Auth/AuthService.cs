using Consolidation.Application.Ports;
using Microsoft.Extensions.Options;
using Shared.Auth.Contracts;
using Shared.Auth.Security;

namespace Consolidation.Infrastructure.Auth;

internal sealed class AuthService(IOptions<JwtOptions> options, IJwtTokenService tokenService) : IAuthService {
    
    public LoginResponse auth(LoginRequest loginRequest)
    {
        var jwt = options.Value;

        var ok = jwt.Users.Any(u =>
            string.Equals(u.Username, loginRequest.Username, StringComparison.Ordinal) &&
            string.Equals(u.Password, loginRequest.Password, StringComparison.Ordinal));

        if (!ok)
            throw new UnauthorizedAccessException();

        var token = tokenService.CreateToken(loginRequest.Username);
        return new LoginResponse(token, jwt.ExpirationMinutes * 60);
    }
}