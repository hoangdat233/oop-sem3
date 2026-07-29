using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatters;

// Formatter that accumulates markdown text and always writes to output.md.
public class FileFormatter : IMessageFormatter
{
    private readonly System.Text.StringBuilder _content;

    public FileFormatter()
    {
        _content = new System.Text.StringBuilder();
    }

    public void WriteTitle(string title)
    {
        _content.AppendLine("# " + title);
    }

    public void WriteBody(string body)
    {
        _content.AppendLine(body);
    }

    public string GetContent()
    {
        return _content.ToString();
    }

    public void Clear()
    {
        _content.Clear();
    }

    public void Save()
    {
        File.WriteAllText("output.md", _content.ToString());
    }
}
