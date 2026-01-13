using Shared.Auth.Contracts;

namespace Ledger.Application.Ports;

public interface IAuthService
{
    LoginResponse auth(LoginRequest loginRequest);
}