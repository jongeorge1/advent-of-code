namespace AdventOfCode.Year2025.Day07;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        (int X, int Y) startLocation = (input[0].IndexOf('S'), 0);
        int maxRow = input.Length - 1;
        int maxCol = input[0].Length - 1;

        (int X, int Y)[] splitters = [.. input
            .SelectMany((row, rowIndex) => row.Select((col, colIndex) => (col, (colIndex, rowIndex))))
            .Where(t => t.col == '^')
            .Select(x => x.Item2)];

        Stack<(int X, int Y)> locationsToProcess = [];
        locationsToProcess.Push(startLocation);
        Dictionary<(int X, int Y), long> realityCountsFromLocations = [];
        long realityCount = GetRealityCountFrom(startLocation, splitters, maxCol, maxRow, realityCountsFromLocations);
        return realityCount.ToString();
    }

    private static long GetRealityCountFrom((int X, int Y) location, (int X, int Y)[] splitters, int maxX, int maxY, Dictionary<(int X, int Y), long> realityCountsFromLocations)
    {
        // If we've already calculated the reality count from this location, return it
        if (realityCountsFromLocations.ContainsKey(location))
        {
            return realityCountsFromLocations[location];
        }

        // If location is out of bounds, return 1.
        if (location.X < 0 || location.X > maxX || location.Y < 0 || location.Y > maxY)
        {
            return 1;
        }

        (int X, int Y) nextLocation = Direction2D.South.GetNextLocation(location);
        if (splitters.Contains(nextLocation))
        {
            // Recursive case: calculate the reality count for both new directions
            long eastCount = GetRealityCountFrom(Direction2D.East.GetNextLocation(nextLocation), splitters, maxX, maxY, realityCountsFromLocations);
            long westCount = GetRealityCountFrom(Direction2D.West.GetNextLocation(nextLocation), splitters, maxX, maxY, realityCountsFromLocations);
            long totalCount = eastCount + westCount;
            realityCountsFromLocations[location] = totalCount;
            return totalCount;
        }

        // Continue moving south
        long count = GetRealityCountFrom(nextLocation, splitters, maxX, maxY, realityCountsFromLocations);
        realityCountsFromLocations[location] = count;
        return count;
    }
}
