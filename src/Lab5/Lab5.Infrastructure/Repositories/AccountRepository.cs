using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Interfaces;

namespace Lab5.Lab5.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts = new();
    private readonly object _lock = new();

    public Task<Account?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            Account? account = _accounts.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(account is null ? null : Copy(account));
        }
    }

    public Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        lock (_lock)
        {
            Account? account = _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            return Task.FromResult(account is null ? null : Copy(account));
        }
    }

    public Task<IEnumerable<Account>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(_accounts.Select(Copy).AsEnumerable());
        }
    }

    public Task AddAsync(Account account)
    {
        lock (_lock)
        {
            _accounts.Add(Copy(account));
            return Task.CompletedTask;
        }
    }

    public Task UpdateAsync(Account account)
    {
        lock (_lock)
        {
            int index = _accounts.FindIndex(a => a.Id == account.Id);
            if (index >= 0)
            {
                _accounts[index] = Copy(account);
            }

            return Task.CompletedTask;
        }
    }

    public Task<bool> ExistsAsync(string accountNumber)
    {
        lock (_lock)
        {
            return Task.FromResult(_accounts.Any(a => a.AccountNumber == accountNumber));
        }
    }

    private static Account Copy(Account account) =>
        new(account.Id, account.AccountNumber, account.PinCode, account.Balance, account.CreatedAt);
}