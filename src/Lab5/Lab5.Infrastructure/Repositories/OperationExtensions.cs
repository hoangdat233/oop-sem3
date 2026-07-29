using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Infrastructure.Repositories;

internal static class OperationExtensions
{
    public static Operation Copy(this Operation operation) =>
        new(operation.AccountId, operation.Type, operation.Amount, operation.BalanceAfter, operation.Description);
}