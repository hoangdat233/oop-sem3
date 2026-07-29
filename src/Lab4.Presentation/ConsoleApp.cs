using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParser;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.ConsoleOutput;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class ConsoleApp
{
    private ApplicationState _state = new ApplicationState();
    private DefaultCommandParser _parser = new DefaultCommandParser();
    private ConsoleOutputWriter _output = new ConsoleOutputWriter();
    private TreePrinter _treePrinter = new TreePrinter(new ConsoleOutputWriter(), new LocalFileSystem());

    public void Run()
    {
        _output.WriteLine("File System Manager - Type 'exit' to quit");

        while (true)
        {
            try
            {
                _output.Write(_state.IsConnected ? $"{_state.CurrentPath}> " : "> ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                ICommand? command = _parser.Parse(input, _state);
                if (command == null)
                {
                    _output.WriteLine("Invalid command");
                    continue;
                }

                CommandResult result = command.Execute();

                if (result.Success)
                {
                    if (!string.IsNullOrEmpty(result.Message))
                        _output.WriteLine(result.Message);

                    if (result.Data is IEnumerable<FileSystemNode> nodes)
                    {
                        int depth = 1;
                        if (command is TreeListCommand treeListCmd)
                        {
                            System.Reflection.FieldInfo? depthField = typeof(TreeListCommand)
                                .GetField("_depth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            if (depthField is not null)
                            {
                                object? value = depthField.GetValue(treeListCmd);
                                if (value is int d)
                                {
                                    depth = d;
                                }
                            }
                        }

                        _treePrinter.PrintTree(nodes, depth);
                    }
                    else if (result.Data is string content)
                    {
                        _output.WriteLine(content);
                    }
                }
                else
                {
                    _output.WriteLine($"Error: {result.Message}");
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void Initialize()
    {
        _state = new ApplicationState();
        _parser = new DefaultCommandParser();
        _output = new ConsoleOutputWriter();
        _treePrinter = new TreePrinter(_output, new LocalFileSystem());

        _state.OnConnected = (basePath, currentPath) =>
        {
            _state.Connect(basePath, currentPath, new LocalFileSystem());
        };

        _state.OnDisconnected = () => _state.Disconnect();
        _state.OnPathChanged = (newPath) => _state.ChangeCurrentPath(newPath);
    }

    public ConsoleApp()
    {
        Initialize();
    }
}
