namespace Itmo.ObjectOrientedProgramming.Lab1;

public abstract class RouteSegment
{
    public double Length { get; }

    public abstract string Type { get; }

    protected RouteSegment(double length)
    {
        Length = length;
    }

    public abstract (bool Success, double Time) PassThrough(Train train);
}