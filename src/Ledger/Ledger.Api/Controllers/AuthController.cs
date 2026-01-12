using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Auth.Contracts;
using Shared.Auth.Security;

namespace Ledger.Api.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController(IOptions<JwtOptions> options, IJwtTokenService tokenService) : ControllerBase
{
    /// <summary>
    /// Issues a JWT token for API access (demo credentials from appsettings).
    /// </summary>
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<LoginResponse> Token([FromBody] LoginRequest request)
    {
        var jwt = options.Value;

        var ok = jwt.Users.Any(u =>
            string.Equals(u.Username, request.Username, StringComparison.Ordinal) &&
            string.Equals(u.Password, request.Password, StringComparison.Ordinal));

        if (!ok)
            return Unauthorized();

        var token = tokenService.CreateToken(request.Username);
        return Ok(new LoginResponse(token, jwt.ExpirationMinutes * 60));
    }
}
