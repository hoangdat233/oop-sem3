namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class DisconnectCommand : ICommand
{
    private readonly Action _onDisconnected;

    public DisconnectCommand(Action onDisconnected)
    {
        _onDisconnected = onDisconnected;
    }

    public CommandResult Execute()
    {
        _onDisconnected?.Invoke();
        return new SuccessResult("Disconnected from file system");
    }
}