using Itmo.ObjectOrientedProgramming.Lab2.Enums;

// Simple DTO representing a message in the system.
namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Message
{
    // Short human-readable title
    public string Title { get; set; }

    // The main text/body of the message
    public string Body { get; set; }

    // Importance level used for filtering
    public MessagePriority Priority { get; set; }

    public Message(string title, string body, MessagePriority priority)
    {
        Title = title;
        Body = body;
        Priority = priority;
    }
}