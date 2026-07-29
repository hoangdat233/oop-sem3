using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.ConsoleOutput;

public interface ITreePrinter
{
    void PrintTree(IEnumerable<FileSystemNode> nodes, int depth);
}

public class TreePrinter : ITreePrinter
{
    private readonly IOutputWriter _output;
    private readonly IFileSystem _fileSystem;

    public TreePrinter(
        IOutputWriter output,
        IFileSystem fileSystem,
        string fileSymbol = "",
        string directorySymbol = "/")
    {
        _output = output;
        _fileSystem = fileSystem;
    }

    public void PrintTree(IEnumerable<FileSystemNode> nodes, int depth)
    {
        PrintTreeRecursive(nodes.ToList(), depth, string.Empty);
    }

    private void PrintTreeRecursive(List<FileSystemNode> nodes, int depth, string prefix)
    {
        if (depth <= 0) return;

        for (int i = 0; i < nodes.Count; i++)
        {
            bool isLast = i == nodes.Count - 1;
            string branch = isLast ? "└── " : "├── ";
            _output.WriteLine(prefix + branch + nodes[i].Name + (nodes[i].IsDirectory ? "/" : string.Empty));

            if (nodes[i].IsDirectory && depth > 1)
            {
                var children = _fileSystem.GetChildren(nodes[i].Path, 1).ToList();
                if (children.Count > 0)
                {
                    string childPrefix = prefix + (isLast ? "    " : "│   ");
                    PrintTreeRecursive(children, depth - 1, childPrefix);
                }
            }
        }
    }
}
