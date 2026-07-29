using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Domain.Interfaces;

public interface ISessionRepository
{
    Task<UserSession?> GetByIdAsync(Guid id);

    Task AddAsync(UserSession session);

    Task UpdateAsync(UserSession session);

    Task<IEnumerable<UserSession>> GetAllAsync();
}