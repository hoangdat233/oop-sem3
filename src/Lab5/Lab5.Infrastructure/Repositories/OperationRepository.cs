using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Interfaces;

namespace Lab5.Lab5.Infrastructure.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly List<Operation> _operations = new();
    private readonly object _lock = new();

    public Task<IEnumerable<Operation>> GetByAccountIdAsync(Guid accountId)
    {
        lock (_lock)
        {
            var operations = _operations
                .Where(o => o.AccountId == accountId)
                .OrderByDescending(o => o.Timestamp)
                .Select(Copy)
                .ToList();

            return Task.FromResult(operations.AsEnumerable());
        }
    }

    public Task AddAsync(Operation operation)
    {
        lock (_lock)
        {
            _operations.Add(Copy(operation));
            return Task.CompletedTask;
        }
    }

    private static Operation Copy(Operation operation) =>
        new(operation.Id, operation.AccountId, operation.Type, operation.Amount, operation.BalanceAfter, operation.Timestamp, operation.Description);
}