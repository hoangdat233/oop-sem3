using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParser;

public class DefaultCommandParser : ICommandParser
{
    private readonly CommandHandler _chain;

    public DefaultCommandParser()
    {
        var connect = new ConnectCommandHandler();
        var disconnect = new DisconnectCommandHandler();
        var treeGoto = new TreeGotoCommandHandler();
        var treeList = new TreeListCommandHandler();
        var fileShow = new FileShowCommandHandler();
        var fileMove = new FileMoveCommandHandler();
        var fileCopy = new FileCopyCommandHandler();
        var fileDelete = new FileDeleteCommandHandler();
        var fileRename = new FileRenameCommandHandler();

        connect.SetNext(disconnect)
            .SetNext(treeGoto)
            .SetNext(treeList)
            .SetNext(fileShow)
            .SetNext(fileMove)
            .SetNext(fileCopy)
            .SetNext(fileDelete)
            .SetNext(fileRename);

        _chain = connect;
    }

    public ICommand? Parse(string input, ApplicationState state)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return null;

        string commandKey = parts[0];
        if (parts.Length > 1 && IsMultiWordCommand($"{parts[0]} {parts[1]}"))
        {
            commandKey = $"{parts[0]} {parts[1]}";
            parts = parts.Skip(2).ToArray();
        }
        else
        {
            parts = parts.Skip(1).ToArray();
        }

        ICommand? cmd = _chain.Handle(commandKey, parts, state);
        if (cmd == null)
            throw new ArgumentException($"Unknown command: {commandKey}");
        return cmd;
    }

    private bool IsMultiWordCommand(string key)
    {
        return key.Equals("tree goto", StringComparison.OrdinalIgnoreCase)
            || key.Equals("tree list", StringComparison.OrdinalIgnoreCase)
            || key.Equals("file show", StringComparison.OrdinalIgnoreCase)
            || key.Equals("file move", StringComparison.OrdinalIgnoreCase)
            || key.Equals("file copy", StringComparison.OrdinalIgnoreCase)
            || key.Equals("file delete", StringComparison.OrdinalIgnoreCase)
            || key.Equals("file rename", StringComparison.OrdinalIgnoreCase);
    }

    private abstract class CommandHandler
    {
        protected CommandHandler? Next { get; private set; }

        public CommandHandler SetNext(CommandHandler next)
        {
            Next = next;
            return next;
        }

        public abstract ICommand? Handle(string commandKey, string[] args, ApplicationState state);
    }

    private sealed class ConnectCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("connect", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length < 1) throw new ArgumentException("Connect command requires address");
                if (state.OnConnected == null) throw new InvalidOperationException("OnConnected callback not set");
                string address = args[0];
                string mode = GetFlagValue(args, "-m") ?? "local";
                return new ConnectCommand(address, mode, state.OnConnected);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private string? GetFlagValue(string[] args, string flag)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals(flag, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }

            return null;
        }
    }

    private sealed class DisconnectCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("disconnect", StringComparison.OrdinalIgnoreCase))
            {
                if (state.OnDisconnected == null) throw new InvalidOperationException("OnDisconnected callback not set");
                return new DisconnectCommand(state.OnDisconnected);
            }

            return Next?.Handle(commandKey, args, state);
        }
    }

    private sealed class TreeGotoCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("tree goto", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 1) throw new ArgumentException("Tree goto command requires path");
                if (state.OnPathChanged == null) throw new InvalidOperationException("OnPathChanged callback not set");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                return new TreeGotoCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0], state.OnPathChanged);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }
    }

    private sealed class TreeListCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("tree list", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                int depth = int.Parse(GetFlagValue(args, "-d") ?? "1");
                return new TreeListCommand(state.FileSystem, state.CurrentPath, state.BasePath, depth);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }

        private string? GetFlagValue(string[] args, string flag)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals(flag, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }

            return null;
        }
    }

    private sealed class FileShowCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("file show", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 1) throw new ArgumentException("File show command requires path");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                string mode = GetFlagValue(args, "-m") ?? "console";
                return new FileShowCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0], mode);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }

        private string? GetFlagValue(string[] args, string flag)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals(flag, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }

            return null;
        }
    }

    private sealed class FileMoveCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("file move", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 2) throw new ArgumentException("File move command requires source and destination");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                return new FileMoveCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0], args[1]);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }
    }

    private sealed class FileCopyCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("file copy", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 2) throw new ArgumentException("File copy command requires source and destination");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                return new FileCopyCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0], args[1]);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }
    }

    private sealed class FileDeleteCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("file delete", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 1) throw new ArgumentException("File delete command requires path");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                return new FileDeleteCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0]);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }
    }

    private sealed class FileRenameCommandHandler : CommandHandler
    {
        public override ICommand? Handle(string commandKey, string[] args, ApplicationState state)
        {
            if (commandKey.Equals("file rename", StringComparison.OrdinalIgnoreCase))
            {
                ValidateConnected(state);
                if (args.Length < 2) throw new ArgumentException("File rename command requires path and name");
                if (state.FileSystem == null || state.CurrentPath == null || state.BasePath == null)
                    throw new InvalidOperationException("State is not properly initialized");
                return new FileRenameCommand(state.FileSystem, state.CurrentPath, state.BasePath, args[0], args[1]);
            }

            return Next?.Handle(commandKey, args, state);
        }

        private void ValidateConnected(ApplicationState state)
        {
            if (state.FileSystem is null)
                throw new InvalidOperationException("Not connected to any file system");
        }
    }
}