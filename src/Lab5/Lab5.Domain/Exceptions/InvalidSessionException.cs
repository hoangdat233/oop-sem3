namespace Lab5.Lab5.Domain.Exceptions;

public class InvalidSessionException : Exception
{
    public InvalidSessionException(string message) : base(message) { }
}