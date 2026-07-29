using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

internal class DummyFileSystem : IFileSystem
{
    public bool Exists(string path) => true;

    public FileSystemNode GetNode(string path)
    {
        return new FileNode("dummy", path);
    }

    public IEnumerable<FileSystemNode> GetChildren(string path, int depth = 1) =>
        Enumerable.Empty<FileSystemNode>();

    public string ReadFile(string path) => string.Empty;

    public void WriteFile(string path, string content) { }

    public void CreateDirectory(string path) { }

    public void Delete(string path) { }

    public void Move(string sourcePath, string destinationPath) { }

    public void Copy(string sourcePath, string destinationPath) { }

    public void Rename(string path, string newName) { }

    public string GetCurrentDirectory() => string.Empty;

    public void SetCurrentDirectory(string path) { }

    public string CombinePath(params string[] paths) => string.Join("/", paths);

    public bool IsAbsolutePath(string path) => path.StartsWith('/');

    public string NormalizePath(string path) => path;
}
