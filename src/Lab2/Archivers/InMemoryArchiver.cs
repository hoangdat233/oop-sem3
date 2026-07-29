using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archivers;

public class InMemoryArchiver : IArchiver
{
    private readonly List<Message> _messages;

    public InMemoryArchiver()
    {
        _messages = new List<Message>();
    }

    public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

    public void ArchiveMessage(Message message)
    {
        _messages.Add(message);
    }

    public void Clear()
    {
        _messages.Clear();
    }
}