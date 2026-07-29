using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class FileDeleteCommand : BaseCommand
{
    private readonly string _path;

    public FileDeleteCommand(IFileSystem fileSystem, string currentPath, string basePath, string path)
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
                return new ErrorResult("Cannot delete a directory with file delete command.");

            FileSystem.Delete(resolvedPath);
            return new SuccessResult($"Deleted {_path}");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to delete file: {ex.Message}");
        }
    }
}