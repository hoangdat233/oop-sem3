using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Interfaces;

namespace Lab5.Lab5.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly List<UserSession> _sessions = new();
    private readonly object _lock = new();

    public Task<UserSession?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            UserSession? session = _sessions.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(session is null ? null : Copy(session));
        }
    }

    public Task AddAsync(UserSession session)
    {
        lock (_lock)
        {
            _sessions.Add(Copy(session));
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(UserSession session)
    {
        lock (_lock)
        {
            int index = _sessions.FindIndex(s => s.Id == session.Id);
            if (index >= 0)
            {
                _sessions[index] = Copy(session);
            }

            return Task.CompletedTask;
        }
    }

    public Task<IEnumerable<UserSession>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(_sessions.Select(Copy).AsEnumerable());
        }
    }

    private static UserSession Copy(UserSession session) =>
        new(session.Id, session.AccountId, session.IsAdmin, session.CreatedAt, session.ExpiresAt);
}
