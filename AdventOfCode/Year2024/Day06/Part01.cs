namespace AdventOfCode.Year2024.Day06;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        var map = Map<char>.CreateCharMap(input);

        (int X, int Y)[] barriers = map.Where(x => x.Value == '#').Select(x => x.Key).ToArray();

        HashSet<(int X, int Y)> visitedLocations = new();
        (int X, int Y) currentLocation = map.First(x => x.Value == '^').Key;
        Direction2D currentDirection = Direction2D.North;

        while (true)
        {
            visitedLocations.Add(currentLocation);

            (int X, int Y) nextLocation = currentDirection.GetNextLocation(currentLocation);

            if (barriers.Contains(nextLocation))
            {
                currentDirection = currentDirection.Right;
                nextLocation = currentDirection.GetNextLocation(currentLocation);
            }

            if (!map.IsLocationInBounds(nextLocation))
            {
                return visitedLocations.Count.ToString();
            }

            currentLocation = nextLocation;
        }
    }
}
