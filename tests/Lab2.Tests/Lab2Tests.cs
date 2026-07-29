using Itmo.ObjectOrientedProgramming.Lab2.Archivers;
using Itmo.ObjectOrientedProgramming.Lab2.Enums;
using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class Lab2Tests
{
    private class TestLogger : ILogger
    {
        public List<string> Logs { get; } = new();

        public void Log(string message) => Logs.Add(message);
    }

    private class TestFormatter : IMessageFormatter
    {
        public List<string> Output { get; } = new();

        public void WriteTitle(string title) => Output.Add($"# {title}");

        public void WriteBody(string body) => Output.Add(body);
    }

    private class MockRecipient : IRecipient
    {
        public int ReceiveCount { get; private set; }

        public Message? LastMessage { get; private set; }

        public void ReceiveMessage(Message message)
        {
            ReceiveCount++;
            LastMessage = message;
        }
    }

    [Fact]
    public void User_ReceivesMessage_StatusIsUnread()
    {
        var user = new User("Alice");
        var message = new Message("Welcome", "Hello Alice!", MessagePriority.Normal);
        user.ReceiveMessage(message);

        Assert.Single(user.ReceivedMessages);
        Assert.Equal(MessageStatus.Unread, user.ReceivedMessages[0].Status);
    }

    [Fact]
    public void User_MarkUnreadMessageAsRead_StatusChanges()
    {
        var user = new User("Bob");
        var message = new Message("Reminder", "Don't forget the meeting.", MessagePriority.High);
        user.ReceiveMessage(message);

        user.MarkMessageAsRead(0);

        Assert.Equal(MessageStatus.Read, user.ReceivedMessages[0].Status);
    }

    [Fact]
    public void User_MarkReadMessageAsRead_Throws()
    {
        var user = new User("Charlie");
        var message = new Message("Update", "Project deadline moved.", MessagePriority.Critical);
        user.ReceiveMessage(message);
        user.MarkMessageAsRead(0);

        Assert.Throws<InvalidOperationException>(() => user.MarkMessageAsRead(0));
    }

    [Fact]
    public void FilteredRecipient_MessageBelowPriority_NotDeliveredToMockRecipient()
    {
        var mockRecipient = new MockRecipient();
        var filtered = new FilteredRecipient(mockRecipient, MessagePriority.High);

        var message = new Message("LowPriority", "This should not be delivered.", MessagePriority.Normal);
        filtered.ReceiveMessage(message);

        Assert.Equal(0, mockRecipient.ReceiveCount);
        Assert.Null(mockRecipient.LastMessage);
    }

    [Fact]
    public void LoggedRecipient_LogsOnReceive()
    {
        var user = new User("Eve");
        var recipient = new UserRecipient(user);
        var logger = new TestLogger();
        var logged = new LoggedRecipient(recipient, logger);

        var message = new Message("Security", "Password changed successfully.", MessagePriority.Normal);
        logged.ReceiveMessage(message);

        Assert.Contains("Message received: Security", logger.Logs);
    }

    [Fact]
    public void FormattingArchiver_CallsFormatter()
    {
        var formatter = new TestFormatter();
        var archiver = new FormattingArchiver(formatter);

        var message = new Message("Report", "Monthly sales report attached.", MessagePriority.Normal);
        archiver.ArchiveMessage(message);

        Assert.Contains("# Report", formatter.Output);
        Assert.Contains("Monthly sales report attached.", formatter.Output);
    }

    [Fact]
    public void GroupRecipient_Filtered_UserReceivesOnce()
    {
        var user = new User("Frank");
        var recipient1 = new UserRecipient(user);
        var recipient2 = new FilteredRecipient(new UserRecipient(user), MessagePriority.High);

        var group = new GroupRecipient();
        group.AddRecipient(recipient1);
        group.AddRecipient(recipient2);

        var message = new Message("Team", "Team lunch at 12pm.", MessagePriority.Normal);
        group.ReceiveMessage(message);

        Assert.Single(user.ReceivedMessages);
    }
}

