namespace AdventOfCode.Year2024.Day06;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    private const char BarrierSymbol = '#';
    private const char StartSymbol = '^';

    public string Solve(string[] input)
    {
        var map = new InvertedMap<char>(Map<char>.CreateCharMap(input));

        (int X, int Y)[] barriers = map['#'];
        (int X, int Y) startLocation = map['^'][0];

        // Only check locations that were visited in part 1
        (int X, int Y)[] possibleBarrierLocations = GetVisitedLocationsForPathWithoutObstacles(map);
        possibleBarrierLocations = possibleBarrierLocations.Where(x => x != startLocation).ToArray();

        // Now try putting a barrier in each of these locations.
        // Could probably increase the efficiency of this approach by walking the path without obstacles and at every step, branch off to see if a loop would
        // be created by putting a barrier in the next location. This would avoid repeatedly walking the increasingly large part of the path before the
        // new obstacle.
        int locationsThatWillCauseALoop = possibleBarrierLocations.AsParallel().Count(possibleBarrierLocation => WillPathLoop(possibleBarrierLocation, map));
        return locationsThatWillCauseALoop.ToString();
    }

    private static (int X, int Y)[] GetVisitedLocationsForPathWithoutObstacles(InvertedMap<char> map)
    {
        HashSet<(int X, int Y)> visitedLocations = new();
        (int X, int Y) currentLocation = map[StartSymbol][0];
        Direction2D currentDirection = Direction2D.North;

        while (true)
        {
            visitedLocations.Add(currentLocation);

            (int X, int Y) nextLocation = currentDirection.GetNextLocation(currentLocation);

            while (map[BarrierSymbol].Contains(nextLocation))
            {
                currentDirection = currentDirection.Right;
                nextLocation = currentDirection.GetNextLocation(currentLocation);
            }

            if (!map.IsLocationInBounds(nextLocation))
            {
                return visitedLocations.ToArray();
            }

            currentLocation = nextLocation;
        }
    }

    private static bool WillPathLoop((int X, int Y) extraBarrierLocation, InvertedMap<char> map)
    {
        List<((int X, int Y) Location, Direction2D Direction)> visitedLocationsWithDirections = new();
        (int X, int Y) currentLocation = map[StartSymbol][0];
        Direction2D currentDirection = Direction2D.North;

        while (true)
        {
            if (visitedLocationsWithDirections.Contains((currentLocation, currentDirection)))
            {
                return true;
            }

            visitedLocationsWithDirections.Add((currentLocation, currentDirection));

            (int X, int Y) nextLocation = currentDirection.GetNextLocation(currentLocation);

            while (map[BarrierSymbol].Contains(nextLocation) || extraBarrierLocation == nextLocation)
            {
                currentDirection = currentDirection.Right;
                nextLocation = currentDirection.GetNextLocation(currentLocation);
            }

            if (!map.IsLocationInBounds(nextLocation))
            {
                return false;
            }

            currentLocation = nextLocation;
        }
    }
}
