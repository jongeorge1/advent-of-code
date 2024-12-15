namespace AdventOfCode.Year2024.Day15;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int split = Array.IndexOf(input, string.Empty);
        string[] mapInput = input[..split];
        string[] instructions = input[(split + 1)..];

        var map = Map<char>.CreateCharMap(mapInput);
        var invertedMap = new InvertedMap<char>(map);

        (int X, int Y) robotLocation = invertedMap['@'][0];
        (int X, int Y)[] wallLocations = invertedMap['#'];
        List<(int X, int Y)> crateLocations = [..invertedMap['O']];

        foreach (string row in instructions)
        {
            foreach (char instruction in row)
            {
                Direction2D direction = Direction2D.ArrowToDirectionMap[instruction];

                if (CanMoveItem(direction, robotLocation, wallLocations, crateLocations))
                {
                    robotLocation = DoMoveRobot(direction, robotLocation, wallLocations, crateLocations);
                }
            }
        }

        return crateLocations.Sum(location => (location.Y * 100) + location.X).ToString();
    }

    private static bool CanMoveItem(Direction2D direction, (int X, int Y) itemLocation, (int X, int Y)[] wallLocations, List<(int X, int Y)> crateLocations)
    {
        (int X, int Y) destination = direction.GetNextLocation(itemLocation);

        if (wallLocations.Contains(destination))
        {
            return false;
        }

        if (crateLocations.Contains(destination))
        {
            return CanMoveItem(direction, destination, wallLocations, crateLocations);
        }

        // Destination is not a wall or a crate, so...
        return true;
    }

    private static (int X, int Y) DoMoveRobot(Direction2D direction, (int X, int Y) itemLocation, (int X, int Y)[] wallLocations, List<(int X, int Y)> crateLocations)
    {
        (int X, int Y) destination = direction.GetNextLocation(itemLocation);

        // We're not going to check whether the destination is a wall; this should already have been done.
        if (crateLocations.Contains(destination))
        {
            DoMoveCrate(direction, destination, wallLocations, crateLocations);
        }

        return destination;
    }

    private static void DoMoveCrate(Direction2D direction, (int X, int Y) crateLocation, (int X, int Y)[] wallLocations, List<(int X, int Y)> crateLocations)
    {
        (int X, int Y) destination = direction.GetNextLocation(crateLocation);

        if (crateLocations.Contains(destination))
        {
            DoMoveCrate(direction, destination, wallLocations, crateLocations);
        }

        crateLocations.Remove(crateLocation);
        crateLocations.Add(destination);
    }
}
