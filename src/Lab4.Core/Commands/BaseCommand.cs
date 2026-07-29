using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public abstract class BaseCommand : ICommand
{
    protected IFileSystem FileSystem { get; }

    protected string CurrentPath { get; }

    protected string BasePath { get; }

    protected BaseCommand(IFileSystem fileSystem, string currentPath, string basePath)
    {
        FileSystem = fileSystem;
        CurrentPath = currentPath;
        BasePath = basePath;
    }

    public abstract CommandResult Execute();

    protected string ResolvePath(string path)
    {
        string resolved;

        if (path.StartsWith('/') || path.StartsWith('\\'))
        {
            string relative = path.TrimStart('/', '\\')
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            resolved = FileSystem.CombinePath(BasePath, relative);
            resolved = FileSystem.NormalizePath(resolved);
        }
        else
        {
            resolved = FileSystem.CombinePath(
                CurrentPath,
                path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar));

            resolved = FileSystem.NormalizePath(resolved);
        }

        string baseNorm = FileSystem.NormalizePath(BasePath);

        if (!resolved.StartsWith(baseNorm, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Access outside of base path is not allowed.");

        return resolved;
    }
}
