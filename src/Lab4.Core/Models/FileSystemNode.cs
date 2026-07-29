namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

public abstract class FileSystemNode
{
    public string Name { get; protected set; }

    public string Path { get; protected set; }

    public bool IsDirectory { get; protected set; }

    protected FileSystemNode(string name, string path, bool isDirectory)
    {
        Name = name;
        Path = path;
        IsDirectory = isDirectory;
    }
}