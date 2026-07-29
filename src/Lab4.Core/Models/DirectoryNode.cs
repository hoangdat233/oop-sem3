namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

public class DirectoryNode : FileSystemNode
{
    public DirectoryNode(string name, string path)
        : base(name, path, true)
    {
    }
}
