using Lab5.Lab5.Application.DTOs;

namespace Lab5.Lab5.Application.Interfaces;

public interface ISessionService
{
    Task<SessionDto> CreateUserSessionAsync(UserLoginRequest request);

    Task<SessionDto> CreateAdminSessionAsync(AdminLoginRequest request, string adminPassword);

    Task<bool> ValidateSessionAsync(Guid sessionId, bool requireAdmin = false);

    Task<Guid?> GetAccountIdFromSessionAsync(Guid sessionId);
}