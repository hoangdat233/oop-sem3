namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Station : RouteSegment
{
    public double SpeedLimit { get; }

    public double BoardingTime { get; }

    public override string Type => "Station";

    public Station(double speedLimit, double boardingTime) : base(0)
    {
        SpeedLimit = speedLimit;
        BoardingTime = boardingTime;
    }

    public override (bool Success, double Time) PassThrough(Train train)
    {
        if (train.Speed > SpeedLimit)
            return (false, 0);

        double arrivalSpeed = train.Speed;

        train.UpdateSpeed(0);

        double stationTime = BoardingTime;

        train.UpdateSpeed(arrivalSpeed);

        return (true, stationTime);
    }
}
