namespace AdventOfCode.Year2024.Day06;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int maxX = input[0].Length - 1;
        int maxY = input.Length - 1;

        ((int X, int Y) Location, char Space)[] map = input.SelectMany((row, rowIndex) => row.Select((col, colIndex) => ((colIndex, rowIndex), col))).ToArray();

        (int X, int Y)[] barriers = map.Where(x => x.Space == '#').Select(x => x.Location).ToArray();

        HashSet<(int X, int Y)> visitedLocations = new();
        (int X, int Y) currentLocation = map.First(x => x.Space == '^').Location;
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

            if (nextLocation.X < 0 || nextLocation.X > maxX || nextLocation.Y < 0 || nextLocation.Y > maxY)
            {
                return visitedLocations.Count.ToString();
            }

            currentLocation = nextLocation;
        }
    }
}
