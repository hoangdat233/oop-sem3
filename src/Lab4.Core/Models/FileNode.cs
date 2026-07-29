namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

public class FileNode : FileSystemNode
{
    public FileNode(string name, string path)
        : base(name, path, false)
    {
    }
}
