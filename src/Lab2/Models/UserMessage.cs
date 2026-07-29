using Itmo.ObjectOrientedProgramming.Lab2.Enums;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class UserMessage
{
    public Message Message { get; }

    public MessageStatus Status { get; private set; }

    public UserMessage(Message message)
    {
        Message = message;
        Status = MessageStatus.Unread;
    }

    public void MarkAsRead()
    {
        if (Status == MessageStatus.Read)
            throw new InvalidOperationException("Message is already read");

        Status = MessageStatus.Read;
    }
}