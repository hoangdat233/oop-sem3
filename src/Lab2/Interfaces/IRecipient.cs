using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IRecipient
{
    void ReceiveMessage(Message message);
}

public interface ILogger
{
    void Log(string message);
}