using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class FileShowCommand : BaseCommand
{
    private readonly string _path;

    public FileShowCommand(
        IFileSystem fileSystem,
        string currentPath,
        string basePath,
        string path,
        string mode)
        : base(fileSystem, currentPath, basePath)
    {
        _path = path;
    }

    public override CommandResult Execute()
    {
        try
        {
            string resolvedPath = ResolvePath(_path);

            if (!FileSystem.Exists(resolvedPath))
                return new ErrorResult($"File does not exist: {_path}");

            FileSystemNode node = FileSystem.GetNode(resolvedPath);
            if (node.IsDirectory)
                return new ErrorResult($"Path is a directory, not a file: {_path}");

            string content = FileSystem.ReadFile(resolvedPath);
            return new SuccessResult("File content", content);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to read file: {ex.Message}");
        }
    }
}