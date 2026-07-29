namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class SuccessResult : CommandResult
{
    public SuccessResult(string message = "", object? data = null)
        : base(true, message, data)
    {
    }
}
