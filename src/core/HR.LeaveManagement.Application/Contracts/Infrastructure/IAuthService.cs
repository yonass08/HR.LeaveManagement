using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Contracts.Infrastructure;

public interface IAuthService
{
    public Task<RegistrationResponse> Register(RegistrationRequest request);

    public Task<AuthResponse> Login(AuthRequest request);
}
