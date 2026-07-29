using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public class LocalFileSystem : IFileSystem
{
    public bool Exists(string path) =>
        File.Exists(path) || Directory.Exists(path);

    public FileSystemNode GetNode(string path)
    {
        if (File.Exists(path))
            return new FileNode(Path.GetFileName(path), path);

        if (Directory.Exists(path))
        {
            string name = Path.GetPathRoot(path)?.TrimEnd(Path.DirectorySeparatorChar)
                == path.TrimEnd(Path.DirectorySeparatorChar)
                ? path.TrimEnd(Path.DirectorySeparatorChar)
                : Path.GetFileName(path);

            if (string.IsNullOrEmpty(name))
                name = path.TrimEnd(Path.DirectorySeparatorChar);

            return new DirectoryNode(name, path);
        }

        throw new FileNotFoundException($"Path not found: {path}");
    }

    public IEnumerable<FileSystemNode> GetChildren(string path, int depth = 1)
    {
        if (depth <= 0) yield break;

        if (!Directory.Exists(path))
            yield break;

        string[] files = Array.Empty<string>();
        string[] dirs = Array.Empty<string>();

        try
        {
            files = Directory.GetFiles(path);
        }
        catch
        {
            // ignore
        }

        try
        {
            dirs = Directory.GetDirectories(path);
        }
        catch
        {
            // ignore
        }

        foreach (string file in files)
            yield return new FileNode(Path.GetFileName(file), file);

        foreach (string dir in dirs)
        {
            string dirName = Path.GetFileName(dir);
            if (dirName.StartsWith("$RECYCLE.BIN", StringComparison.OrdinalIgnoreCase)
                || dirName.StartsWith("System Volume Information", StringComparison.OrdinalIgnoreCase)
                || (new DirectoryInfo(dir).Attributes & FileAttributes.System) != 0
                || (new DirectoryInfo(dir).Attributes & FileAttributes.Hidden) != 0)
            {
                continue;
            }

            yield return new DirectoryNode(dirName, dir);
        }
    }

    public string ReadFile(string path) =>
        File.ReadAllText(path);

    public void WriteFile(string path, string content) =>
        File.WriteAllText(path, content);

    public void CreateDirectory(string path) =>
        Directory.CreateDirectory(path);

    public void Delete(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
        else if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    public void Move(string sourcePath, string destinationPath)
    {
        if (File.Exists(sourcePath))
            File.Move(sourcePath, destinationPath);
        else if (Directory.Exists(sourcePath))
            Directory.Move(sourcePath, destinationPath);
    }

    public void Copy(string sourcePath, string destinationPath)
    {
        if (File.Exists(sourcePath))
            File.Copy(sourcePath, destinationPath);
        else
            throw new InvalidOperationException("Directory copy not implemented");
    }

    public void Rename(string path, string newName)
    {
        string? dir = Path.GetDirectoryName(path);
        string newPath = Path.Combine(dir ?? string.Empty, newName);
        Move(path, newPath);
    }

    public string GetCurrentDirectory() =>
        Directory.GetCurrentDirectory();

    public void SetCurrentDirectory(string path) =>
        Directory.SetCurrentDirectory(path);

    public string CombinePath(params string[] paths) =>
        Path.Combine(paths);

    public bool IsAbsolutePath(string path) =>
        Path.IsPathRooted(path);

    public string NormalizePath(string path)
    {
        string normalized = Path.GetFullPath(path);
        return normalized.TrimEnd(Path.DirectorySeparatorChar);
    }
}
