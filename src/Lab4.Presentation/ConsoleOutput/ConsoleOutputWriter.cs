namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.ConsoleOutput;

public class ConsoleOutputWriter : IOutputWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }
}
