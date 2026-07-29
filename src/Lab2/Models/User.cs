// Represents an application user who can receive and manage messages.
namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class User
{
    public string Name { get; }

    private readonly List<UserMessage> _receivedMessages;

    public IReadOnlyList<UserMessage> ReceivedMessages => _receivedMessages.AsReadOnly();

    public User(string name)
    {
        Name = name;
        _receivedMessages = new List<UserMessage>();
    }

    // Receives a message and stores it as unread for the user.
    public void ReceiveMessage(Message message)
    {
        _receivedMessages.Add(new UserMessage(message));
    }

    public void MarkMessageAsRead(int messageIndex)
    {
        if (messageIndex < 0 || messageIndex >= _receivedMessages.Count)
            throw new ArgumentOutOfRangeException(nameof(messageIndex));

        _receivedMessages[messageIndex].MarkAsRead();
    }
}