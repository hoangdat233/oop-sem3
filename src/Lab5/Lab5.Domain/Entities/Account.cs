namespace Lab5.Lab5.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }

    public string AccountNumber { get; private set; }

    public string PinCode { get; private set; }

    public decimal Balance { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Account(string accountNumber, string pinCode, decimal initialBalance = 0)
    {
        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        PinCode = pinCode;
        Balance = initialBalance;
        CreatedAt = DateTime.UtcNow;
    }

    internal Account(Guid id, string accountNumber, string pinCode, decimal balance, DateTime createdAt)
    {
        Id = id;
        AccountNumber = accountNumber;
        PinCode = pinCode;
        Balance = balance;
        CreatedAt = createdAt;
    }

    private Account()
    {
        AccountNumber = string.Empty;
        PinCode = string.Empty;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds");

        Balance -= amount;
    }
}