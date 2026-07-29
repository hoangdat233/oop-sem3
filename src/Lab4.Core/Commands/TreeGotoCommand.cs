using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class TreeGotoCommand : BaseCommand
{
    private readonly string _path;
    private readonly Action<string> _onPathChanged;

    public TreeGotoCommand(
        IFileSystem fileSystem,
        string currentPath,
        string basePath,
        string path,
        Action<string> onPathChanged)
        : base(fileSystem, currentPath, basePath)
    {
        _path = path;
        _onPathChanged = onPathChanged;
    }

    public override CommandResult Execute()
    {
        try
        {
            string resolvedPath = ResolvePath(_path);

            if (!FileSystem.Exists(resolvedPath))
                return new ErrorResult($"Path does not exist: {_path}");

            FileSystemNode node = FileSystem.GetNode(resolvedPath);
            if (!node.IsDirectory)
                return new ErrorResult($"Path is not a directory: {_path}");

            _onPathChanged?.Invoke(resolvedPath);
            return new SuccessResult($"Changed directory to {resolvedPath}");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to change directory: {ex.Message}");
        }
    }
}