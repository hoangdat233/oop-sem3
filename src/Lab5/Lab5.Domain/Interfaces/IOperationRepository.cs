using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Domain.Interfaces;

public interface IOperationRepository
{
    Task<IEnumerable<Operation>> GetByAccountIdAsync(Guid accountId);

    Task AddAsync(Operation operation);
}