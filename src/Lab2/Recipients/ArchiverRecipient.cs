using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class ArchiverRecipient : IRecipient
{
    private readonly IArchiver _archiver;

    public ArchiverRecipient(IArchiver archiver)
    {
        _archiver = archiver ?? throw new ArgumentNullException(nameof(archiver));
    }

    public void ReceiveMessage(Message message)
    {
        _archiver.ArchiveMessage(message);
    }
}