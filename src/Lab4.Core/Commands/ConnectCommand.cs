using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class ConnectCommand : ICommand
{
    private readonly string _address;
    private readonly Action<string, string> _onConnected;

    public ConnectCommand(string address, string mode, Action<string, string> onConnected)
    {
        _address = address;
        _onConnected = onConnected;
    }

    public CommandResult Execute()
    {
        try
        {
            string normalizedPath = Path.GetFullPath(_address);

            if (normalizedPath.Length == 3 && normalizedPath[1] == ':' && normalizedPath[2] == '\\')
            {
                normalizedPath = normalizedPath.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
            }

            var fileSystem = new LocalFileSystem();

            if (!fileSystem.Exists(normalizedPath))
                return new ErrorResult($"Path does not exist: {_address}");

            _onConnected?.Invoke(normalizedPath, normalizedPath);

            return new SuccessResult($"Connected to {normalizedPath}");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Connection failed: {ex.Message}");
        }
    }
}