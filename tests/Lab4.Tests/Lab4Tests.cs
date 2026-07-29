using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParser;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class Lab4Tests
{
    [Fact]
    public void Parser_Should_Parse_Connect_Command()
    {
        var state = new ApplicationState();
        state.OnConnected = (basePath, currentPath) => { };
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("connect /tmp -m local", state);
        Assert.IsType<ConnectCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Throw_On_Unknown_Command()
    {
        var state = new ApplicationState();
        var parser = new DefaultCommandParser();
        Assert.Throws<ArgumentException>(() => parser.Parse("unknowncmd", state));
    }

    [Fact]
    public void Parser_Should_Parse_TreeGoto_Command()
    {
        var state = new ApplicationState();
        state.OnPathChanged = _ => { };
        var parser = new DefaultCommandParser();

        Assert.Throws<InvalidOperationException>(() => parser.Parse("tree goto folder", state));

        state.Connect("/base", "/base", new DummyFileSystem());
        ICommand? cmd = parser.Parse("tree goto folder", state);
        Assert.IsType<TreeGotoCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_TreeList_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("tree list -d 2", state);
        Assert.IsType<TreeListCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_FileShow_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("file show file.txt -m console", state);
        Assert.IsType<FileShowCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_FileMove_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("file move a.txt b", state);
        Assert.IsType<FileMoveCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_FileCopy_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("file copy a.txt b", state);
        Assert.IsType<FileCopyCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_FileDelete_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("file delete a.txt", state);
        Assert.IsType<FileDeleteCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Parse_FileRename_Command()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();
        ICommand? cmd = parser.Parse("file rename a.txt b.txt", state);
        Assert.IsType<FileRenameCommand>(cmd);
    }

    [Fact]
    public void Parser_Should_Throw_On_Missing_Required_Arguments()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();

        Assert.Throws<ArgumentException>(() => parser.Parse("file move a.txt", state));
        Assert.Throws<ArgumentException>(() => parser.Parse("file copy", state));
        Assert.Throws<ArgumentException>(() => parser.Parse("file delete", state));
        Assert.Throws<ArgumentException>(() => parser.Parse("file rename a.txt", state));
        Assert.Throws<ArgumentException>(() => parser.Parse("tree goto", state));
        Assert.Throws<ArgumentException>(() => parser.Parse("file show", state));
    }

    [Fact]
    public void Parser_Should_Throw_On_Missing_Flags()
    {
        var state = new ApplicationState();
        state.Connect("/base", "/base", new DummyFileSystem());
        var parser = new DefaultCommandParser();

        ICommand? cmd = parser.Parse("tree list", state);
        Assert.IsType<TreeListCommand>(cmd);

        cmd = parser.Parse("file show file.txt", state);
        Assert.IsType<FileShowCommand>(cmd);
    }
}
