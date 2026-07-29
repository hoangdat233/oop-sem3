using Itmo.ObjectOrientedProgramming.Lab2.AlertSystems;
using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public class AlertSystemRecipient : IRecipient
{
    private readonly IAlertSystem _alertSystem;
    private readonly string[] _suspiciousWords;

    public AlertSystemRecipient(IAlertSystem alertSystem, params string[] suspiciousWords)
    {
        _alertSystem = alertSystem ?? throw new ArgumentNullException(nameof(alertSystem));
        _suspiciousWords = suspiciousWords;
    }

    public void ReceiveMessage(Message message)
    {
        string messageText = $"{message.Title} {message.Body}".ToLowerInvariant();

        if (_suspiciousWords.Any(word =>
            messageText.Contains(word.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase)))
        {
            _alertSystem.TriggerAlert();
        }
    }
}