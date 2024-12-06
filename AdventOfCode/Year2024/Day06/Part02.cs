namespace AdventOfCode.Year2024.Day06;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        int maxX = input[0].Length - 1;
        int maxY = input.Length - 1;

        ((int X, int Y) Location, char Space)[] map = input.SelectMany((row, rowIndex) => row.Select((col, colIndex) => ((colIndex, rowIndex), col))).ToArray();

        (int X, int Y)[] barriers = map.Where(x => x.Space == '#').Select(x => x.Location).ToArray();
        (int X, int Y) startLocation = map.First(x => x.Space == '^').Location;

        // Only check locations that were visited in part 1
        (int X, int Y)[] possibleBarrierLocations = GetVisitedLocationsForPathWithoutObstacles(startLocation, barriers, maxX, maxY);
        possibleBarrierLocations = possibleBarrierLocations.Where(x => x != startLocation).ToArray();

        // Now try putting a barrier in each of these locations.
        // Could probably increase the efficiency of this approach by walking the path without obstacles and at every step, branch off to see if a loop would
        // be created by putting a barrier in the next location. This would avoid repeatedly walking the increasingly large part of the path before the
        // new obstacle.
        int locationsThatWillCauseALoop = possibleBarrierLocations.AsParallel().Count(possibleBarrierLocations => WillPathLoop(startLocation, barriers, possibleBarrierLocations, maxX, maxY));
        return locationsThatWillCauseALoop.ToString();
    }

    private static (int X, int Y)[] GetVisitedLocationsForPathWithoutObstacles((int X, int Y) startLocation, (int X, int Y)[] barrierLocations, int maxX, int maxY)
    {
        HashSet<(int X, int Y)> visitedLocations = new();
        (int X, int Y) currentLocation = startLocation;
        Direction2D currentDirection = Direction2D.North;

        while (true)
        {
            visitedLocations.Add(currentLocation);

            (int X, int Y) nextLocation = currentDirection.GetNextLocation(currentLocation);

            while (barrierLocations.Contains(nextLocation))
            {
                currentDirection = currentDirection.Right;
                nextLocation = currentDirection.GetNextLocation(currentLocation);
            }

            if (nextLocation.X < 0 || nextLocation.X > maxX || nextLocation.Y < 0 || nextLocation.Y > maxY)
            {
                return visitedLocations.ToArray();
            }

            currentLocation = nextLocation;
        }
    }

    private static bool WillPathLoop((int X, int Y) startLocation, (int X, int Y)[] barrierLocations, (int X, int Y) extraBarrierLocation, int maxX, int maxY)
    {
        List<((int X, int Y) Location, Direction2D Direction)> visitedLocationsWithDirections = new();
        (int X, int Y) currentLocation = startLocation;
        Direction2D currentDirection = Direction2D.North;

        while (true)
        {
            if (visitedLocationsWithDirections.Contains((currentLocation, currentDirection)))
            {
                return true;
            }

            visitedLocationsWithDirections.Add((currentLocation, currentDirection));

            (int X, int Y) nextLocation = currentDirection.GetNextLocation(currentLocation);

            while (barrierLocations.Contains(nextLocation) || extraBarrierLocation == nextLocation)
            {
                currentDirection = currentDirection.Right;
                nextLocation = currentDirection.GetNextLocation(currentLocation);
            }

            if (nextLocation.X < 0 || nextLocation.X > maxX || nextLocation.Y < 0 || nextLocation.Y > maxY)
            {
                return false;
            }

            currentLocation = nextLocation;
        }
    }
}
