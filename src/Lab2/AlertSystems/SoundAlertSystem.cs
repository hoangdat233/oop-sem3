namespace Itmo.ObjectOrientedProgramming.Lab2.AlertSystems;

public class SoundAlertSystem : IAlertSystem
{
    public void TriggerAlert()
    {
        Console.Beep();
    }
}