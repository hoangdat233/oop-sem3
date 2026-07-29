using Itmo.ObjectOrientedProgramming.Lab2.Enums;
using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

// Forwards messages only if they meet the minimum priority threshold.
public class FilteredRecipient : IRecipient
{
    private readonly IRecipient _recipient;
    private readonly MessagePriority _minPriority;

    public FilteredRecipient(IRecipient recipient, MessagePriority minPriority)
    {
        _recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        _minPriority = minPriority;
    }

    public void ReceiveMessage(Message message)
    {
        if (message.Priority >= _minPriority)
        {
            _recipient.ReceiveMessage(message);
        }
    }
}