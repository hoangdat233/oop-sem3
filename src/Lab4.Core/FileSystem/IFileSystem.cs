using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public interface IFileSystem
{
    bool Exists(string path);

    FileSystemNode GetNode(string path);

    IEnumerable<FileSystemNode> GetChildren(string path, int depth = 1);

    string ReadFile(string path);

    void WriteFile(string path, string content);

    void CreateDirectory(string path);

    void Delete(string path);

    void Move(string sourcePath, string destinationPath);

    void Copy(string sourcePath, string destinationPath);

    void Rename(string path, string newName);

    string GetCurrentDirectory();

    void SetCurrentDirectory(string path);

    string CombinePath(params string[] paths);

    bool IsAbsolutePath(string path);

    string NormalizePath(string path);
}
