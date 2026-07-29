namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class ErrorResult : CommandResult
{
    public ErrorResult(string message)
        : base(false, message)
    {
    }
}
