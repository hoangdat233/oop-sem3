namespace Itmo.ObjectOrientedProgramming.Lab2.AlertSystems;

public class TextAlertSystem : IAlertSystem
{
    private readonly string _alertMessage;

    public TextAlertSystem(string alertMessage)
    {
        _alertMessage = alertMessage;
    }

    public void TriggerAlert()
    {
        Console.WriteLine("ALERT: " + _alertMessage);
    }
}