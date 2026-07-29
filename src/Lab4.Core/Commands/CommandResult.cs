namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public abstract class CommandResult
{
    public bool Success { get; protected set; }

    public string Message { get; protected set; } = string.Empty;

    public object? Data { get; protected set; }

    protected CommandResult(bool success, string message = "", object? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}
