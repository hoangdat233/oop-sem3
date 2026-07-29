using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatters;

// Lightweight formatter that prints markdown-ish output to console — useful during development.
public class ConsoleFormatter : IMessageFormatter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine("# " + title);
    }

    public void WriteBody(string body)
    {
        Console.WriteLine(body);
    }
}