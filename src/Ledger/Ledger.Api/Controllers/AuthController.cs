using Ledger.Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth.Contracts;

namespace Ledger.Api.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController(IAuthService auth) : ControllerBase
{
    /// <summary>
    /// Issues a JWT token for API access (demo credentials from appsettings).
    /// </summary>
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<LoginResponse> Token([FromBody] LoginRequest request)
    {
        try
        {
            return Ok(auth.auth(request));   
        } 
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
