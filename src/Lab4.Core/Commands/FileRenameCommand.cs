using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class FileRenameCommand : BaseCommand
{
    private readonly string _path;
    private readonly string _name;

    public FileRenameCommand(
        IFileSystem fileSystem,
        string currentPath,
        string basePath,
        string path,
        string name)
        : base(fileSystem, currentPath, basePath)
    {
        _path = path;
        _name = name;
    }

    public override CommandResult Execute()
    {
        try
        {
            if (_name.Contains('/', StringComparison.Ordinal) || _name.Contains('\\', StringComparison.Ordinal))
                return new ErrorResult("New name must not contain path separators.");

            string resolvedPath = ResolvePath(_path);

            if (!FileSystem.Exists(resolvedPath))
                return new ErrorResult($"File does not exist: {_path}");

            FileSystem.Rename(resolvedPath, _name);
            return new SuccessResult($"Renamed {_path} to {_name}");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to rename file: {ex.Message}");
        }
    }
}
