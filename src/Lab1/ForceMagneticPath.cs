namespace Itmo.ObjectOrientedProgramming.Lab1;

public class ForceMagneticPath : RouteSegment
{
    public double Force { get; }

    public override string Type => "Force";

    public ForceMagneticPath(double length, double force) : base(length)
    {
        Force = force;
    }

    public override (bool Success, double Time) PassThrough(Train train)
    {
        if (!train.ApplyForce(Force))
            return (false, 0);

        (bool Success, double Time) result = train.CalculateTravelTime(Length);

        train.ResetAcceleration();
        return result;
    }
}
