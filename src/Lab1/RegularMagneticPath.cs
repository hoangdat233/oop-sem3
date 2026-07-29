namespace Itmo.ObjectOrientedProgramming.Lab1;

public class RegularMagneticPath : RouteSegment
{
    public override string Type => "Regular";

    public RegularMagneticPath(double length) : base(length) { }

    public override (bool Success, double Time) PassThrough(Train train)
    {
        return train.CalculateTravelTime(Length);
    }
}
