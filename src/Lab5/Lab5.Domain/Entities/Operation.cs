namespace Lab5.Lab5.Domain.Entities;

public class Operation
{
    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }

    public OperationType Type { get; private set; }

    public decimal Amount { get; private set; }

    public decimal BalanceAfter { get; private set; }

    public DateTime Timestamp { get; private set; }

    public string Description { get; private set; }

    public Operation(Guid accountId, OperationType type, decimal amount, decimal balanceAfter, string description)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;
        Timestamp = DateTime.UtcNow;
        Description = description;
    }

    internal Operation(Guid id, Guid accountId, OperationType type, decimal amount, decimal balanceAfter, DateTime timestamp, string description)
    {
        Id = id;
        AccountId = accountId;
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;
        Timestamp = timestamp;
        Description = description;
    }

    private Operation()
    {
        Description = string.Empty;
    }
}

public enum OperationType
{
    Deposit,
    Withdrawal,
    BalanceCheck,
    AccountCreation,
}