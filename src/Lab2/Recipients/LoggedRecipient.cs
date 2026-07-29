using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

// Decorator: logs incoming messages and forwards to the wrapped recipient.
public class LoggedRecipient : IRecipient
{
    private readonly IRecipient _recipient;
    private readonly ILogger _logger;

    public LoggedRecipient(IRecipient recipient, ILogger logger)
    {
        _recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void ReceiveMessage(Message message)
    {
        _logger.Log($"Message received: {message.Title}");
        _recipient.ReceiveMessage(message);
    }
}