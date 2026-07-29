using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core;

public class ApplicationState
{
    public IFileSystem? FileSystem { get; internal set; }

    public string? CurrentPath { get; internal set; }

    public string? BasePath { get; internal set; }

    public bool IsConnected => FileSystem is not null;

    public Action<string, string>? OnConnected { get; set; }

    public Action? OnDisconnected { get; set; }

    public Action<string>? OnPathChanged { get; set; }

    public void Connect(string basePath, string currentPath, IFileSystem fileSystem)
    {
        BasePath = basePath;
        CurrentPath = currentPath;
        FileSystem = fileSystem;
    }

    public void Disconnect()
    {
        FileSystem = null;
        CurrentPath = null;
        BasePath = null;
    }

    public void ChangeCurrentPath(string newPath)
    {
        CurrentPath = newPath;
    }
}