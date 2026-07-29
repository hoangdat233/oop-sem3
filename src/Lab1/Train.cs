namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Train
{
    public double Mass { get; }

    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public double MaxForce { get; }

    public double Precision { get; }

    public Train(double mass, double maxForce, double precision)
    {
        Mass = mass;
        MaxForce = maxForce;

        Precision = precision > 0 ? precision : 0.000001;
        Speed = 0;
        Acceleration = 0;
    }

    public bool ApplyForce(double force)
    {
        if (Math.Abs(force) > MaxForce)
            return false;

        Acceleration = force / Mass;
        return true;
    }

    public (bool Success, double Time) CalculateTravelTime(double distance)
    {
        if (distance <= 0)
            return (true, 0);

        if (Speed == 0 && Acceleration == 0)
            return (false, 0);

        double currentSpeed = Speed;
        double remainingDistance = distance;
        double totalTime = 0;

        while (remainingDistance > 0)
        {
            double newSpeed = currentSpeed + (Acceleration * Precision);

            if (newSpeed < 0)
            {
                Speed = currentSpeed;
                return (false, totalTime);
            }

            double distanceThisStep = newSpeed * Precision;
            remainingDistance -= distanceThisStep;
            totalTime += Precision;
            currentSpeed = newSpeed;

            if (remainingDistance <= 0)
                break;
        }

        Speed = currentSpeed;
        return (true, totalTime);
    }

    public void UpdateSpeed(double newSpeed) => Speed = newSpeed;

    public void ResetAcceleration() => Acceleration = 0;
}