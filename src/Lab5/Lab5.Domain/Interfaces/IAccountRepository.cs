using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);

    Task<Account?> GetByAccountNumberAsync(string accountNumber);

    Task<IEnumerable<Account>> GetAllAsync();

    Task AddAsync(Account account);

    Task UpdateAsync(Account account);

    Task<bool> ExistsAsync(string accountNumber);
}