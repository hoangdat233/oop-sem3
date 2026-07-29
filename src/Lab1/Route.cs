namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Route
{
    private readonly IReadOnlyList<RouteSegment> segments;

    public double FinalSpeedLimit { get; }

    public Route(double finalSpeedLimit, IEnumerable<RouteSegment> segments)
    {
        this.segments = (segments ?? Enumerable.Empty<RouteSegment>()).ToArray();
        FinalSpeedLimit = finalSpeedLimit;
    }

    public RouteResult Simulate(Train train)
    {
        double totalTime = 0;

        foreach (RouteSegment segment in segments)
        {
            (bool Success, double Time) result = segment.PassThrough(train);

            if (!result.Success)
            {
                string failureReason = $"Failed at {segment.Type} segment";
                return RouteResult.Failure(failureReason);
            }

            totalTime += result.Time;
        }

        if (train.Speed > FinalSpeedLimit)
        {
            return RouteResult.Failure($"Train exceeded final speed limit: {FinalSpeedLimit}");
        }

        return RouteResult.Success(totalTime);
    }
}