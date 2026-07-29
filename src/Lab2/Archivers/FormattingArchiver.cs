using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archivers;

public class FormattingArchiver : IArchiver
{
    private readonly IMessageFormatter _formatter;

    public FormattingArchiver(IMessageFormatter formatter)
    {
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    public void ArchiveMessage(Message message)
    {
        _formatter.WriteTitle(message.Title);
        _formatter.WriteBody(message.Body);
    }
}