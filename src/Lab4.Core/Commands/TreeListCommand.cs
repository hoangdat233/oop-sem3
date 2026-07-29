using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class TreeListCommand : BaseCommand
{
    private readonly int _depth;

    public TreeListCommand(IFileSystem fileSystem, string currentPath, string basePath, int depth)
        : base(fileSystem, currentPath, basePath)
    {
        _depth = depth;
    }

    public override CommandResult Execute()
    {
        try
        {
            var children = FileSystem.GetChildren(CurrentPath, _depth).ToList();
            return new SuccessResult("Directory listing", children);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Failed to list directory: {ex.Message}");
        }
    }
}