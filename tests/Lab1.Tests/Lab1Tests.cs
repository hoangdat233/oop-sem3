using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class Lab1Tests
{
    [Fact]
    public void Scenario1_Success()
    {
        var train = new Train(1000, 500, 0.05);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 500),
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.True(result.IsSuccess);
        Assert.True(result.TotalTime > 0);
    }

    [Fact]
    public void Scenario2_Failure()
    {
        var train = new Train(1000, 500, 0.2);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 1000),
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Scenario3_Success()
    {
        var train = new Train(1000, 500, 0.01);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 500),
            new RegularMagneticPath(20),
            new Station(50, 5),
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.True(result.IsSuccess);
        Assert.True(result.TotalTime > 0);
    }

    [Fact]
    public void Scenario4_Failure()
    {
        var train = new Train(1000, 500, 0.5);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 1000),
            new Station(50, 5),
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Scenario5_Failure()
    {
        var train = new Train(1000, 500, 0.15);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 1000),
            new RegularMagneticPath(20),
            new Station(100, 5),
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Scenario6_Success()
    {
        var train = new Train(1000, 500, 0.07);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 200),
            new RegularMagneticPath(20),
            new ForceMagneticPath(10, -100),
            new Station(50, 5),
            new RegularMagneticPath(20),
            new ForceMagneticPath(10, 200),
            new RegularMagneticPath(20),
            new ForceMagneticPath(10, -100),
        });

        RouteResult result = route.Simulate(train);

        Assert.True(result.IsSuccess);
        Assert.True(result.TotalTime > 0);
    }

    [Fact]
    public void Scenario7_Failure()
    {
        var train = new Train(1000, 500, 0.3);
        var route = new Route(50, new RouteSegment[]
        {
            new RegularMagneticPath(20),
        });

        RouteResult result = route.Simulate(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("Failed at Regular segment", result.FailureReason);
    }

    [Fact]
    public void Scenario8_Failure()
    {
        var train = new Train(1000, 500, 0.12);
        var route = new Route(50, new RouteSegment[]
        {
            new ForceMagneticPath(10, 500),
            new ForceMagneticPath(10, -1000),
        });

        RouteResult result = route.Simulate(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("Failed at Force segment", result.FailureReason);
    }
}