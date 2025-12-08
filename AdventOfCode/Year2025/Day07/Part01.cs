namespace AdventOfCode.Year2025.Day07;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        // Parse the input, finding:
        // 1. The start location (always in row 0)
        // 2. The number of rows (important so we know when to stop tracing a beam
        // 3. The number of columns (important so we know when to stop tracing a beam)
        // 4. The locations of all the splitters
        (int X, int Y) startLocation = (input[0].IndexOf('S'), 0);
        int maxRow = input.Length - 1;
        int maxCol = input[0].Length - 1;

        (int X, int Y)[] splitters = [.. input
            .SelectMany((row, rowIndex) => row.Select((col, colIndex) => (col, (colIndex, rowIndex))))
            .Where(t => t.col == '^')
            .Select(x => x.Item2)];

        List<(int X, int Y)> processedLocations = [];
        Stack<(int X, int Y)> locationsToProcess = [];
        locationsToProcess.Push(startLocation);
        int splitCount = 0;
        Direction2D beamDirection = Direction2D.South;

        while (locationsToProcess.Count > 0)
        {
            (int X, int Y) locationToProcess = locationsToProcess.Pop();
            if (processedLocations.Contains(locationToProcess))
            {
                continue;
            }

            processedLocations.Add(locationToProcess);
            (int X, int Y) nextLocation = beamDirection.GetNextLocation(locationToProcess);

            // Is it a splitter location?
            if (splitters.Contains(nextLocation))
            {
                splitCount++;

                // Add both new directions to the stack
                locationsToProcess.Push(Direction2D.East.GetNextLocation(nextLocation));
                locationsToProcess.Push(Direction2D.West.GetNextLocation(nextLocation));
            }
            else
            {
                // Is it still in bounds?
                if (nextLocation.X >= 0 && nextLocation.X <= maxCol &&
                    nextLocation.Y >= 0 && nextLocation.Y <= maxRow)
                {
                    locationsToProcess.Push(nextLocation);
                }
            }
        }

        return splitCount.ToString();
    }
}
