using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class UserRecipient : IRecipient
{
    private readonly User _user;

    public UserRecipient(User user)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
    }

    public void ReceiveMessage(Message message)
    {
        _user.ReceiveMessage(message);
    }
}