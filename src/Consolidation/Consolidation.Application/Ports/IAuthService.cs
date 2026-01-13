using Shared.Auth.Contracts;

namespace Consolidation.Application.Ports;

public interface IAuthService
{
    LoginResponse auth(LoginRequest loginRequest);
}