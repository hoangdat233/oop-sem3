using Lab5.Lab5.Application.DTOs;
using Lab5.Lab5.Application.Interfaces;
using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Interfaces;

namespace Lab5.Lab5.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IAccountRepository _accountRepository;

    public SessionService(ISessionRepository sessionRepository, IAccountRepository accountRepository)
    {
        _sessionRepository = sessionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<SessionDto> CreateUserSessionAsync(UserLoginRequest request)
    {
        Account? account = await _accountRepository.GetByAccountNumberAsync(request.AccountNumber);

        if (account == null || account.PinCode != request.PinCode)
            throw new UnauthorizedAccessException("Invalid account number or PIN");

        var session = new UserSession(account.Id, false);
        await _sessionRepository.AddAsync(session);

        return MapToDto(session);
    }

    public async Task<SessionDto> CreateAdminSessionAsync(AdminLoginRequest request, string adminPassword)
    {
        if (request.Password != adminPassword)
            throw new UnauthorizedAccessException("Invalid admin password");

        var session = new UserSession(null, true);
        await _sessionRepository.AddAsync(session);

        return MapToDto(session);
    }

    public async Task<bool> ValidateSessionAsync(Guid sessionId, bool requireAdmin = false)
    {
        UserSession? session = await _sessionRepository.GetByIdAsync(sessionId);

        if (session == null || !session.IsValid())
            return false;

        if (requireAdmin && !session.IsAdmin)
            return false;

        return true;
    }

    public async Task<Guid?> GetAccountIdFromSessionAsync(Guid sessionId)
    {
        UserSession? session = await _sessionRepository.GetByIdAsync(sessionId);
        return session?.AccountId;
    }

    private SessionDto MapToDto(UserSession session) => new()
    {
        SessionId = session.Id,
        ExpiresAt = session.ExpiresAt,
        IsAdmin = session.IsAdmin,
    };
}