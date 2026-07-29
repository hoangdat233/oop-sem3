using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParser;

public interface ICommandParser
{
    ICommand? Parse(string input, ApplicationState state);
}