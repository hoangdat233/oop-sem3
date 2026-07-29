namespace Lab5.Lab5.Domain.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string message) : base(message) { }
}