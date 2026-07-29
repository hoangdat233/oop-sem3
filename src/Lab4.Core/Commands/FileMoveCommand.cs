using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class FileMoveCommand : BaseCommand
{
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(
        IFileSystem fileSystem,
        string currentPath,
        string basePath,
        string sourcePath,
        string destinationPath)
        : base(fileSystem, currentPath, basePath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public override CommandResult Execute()
    {
        try
        {
            string resolvedSource = ResolvePath(_sourcePath);
            string resolvedDestination = ResolvePath(_destinationPath);

            if (!FileSystem.Exists(resolvedSource))
                return new ErrorResult($"Source file does not exist: {_sourcePath}");

            if (!FileSystem.Exists(resolvedDestination) || !FileSystem.GetNode(resolvedDestination).IsDirectory)
                return new ErrorResult($"Destination directory does not exist: {_destinationPath}");

            string fileName = Path.GetFileName(resolvedSource);
            string destFile = FileSystem.CombinePath(resolvedDestination, fileName);

            if (FileSystem.Exists(destFile))
                return new ErrorResult($"File already exists at destination: {destFile}");

            FileSystem.Move(resolvedSource, destFile);
            return new SuccessResult($"Moved {_sourcePath} to {_destinationPath}");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to move file: {ex.Message}");
        }
    }
}