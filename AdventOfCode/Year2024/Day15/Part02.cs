namespace AdventOfCode.Year2024.Day15;

using System;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        int split = Array.IndexOf(input, string.Empty);
        string[] mapInput = input[..split];
        string[] instructions = input[(split + 1)..];

        var map = Map<char>.CreateCharMap(mapInput);
        var invertedMap = new InvertedMap<char>(map);

        (int X, int Y) robotLocation = invertedMap['@'][0];
        robotLocation = (robotLocation.X * 2, robotLocation.Y);

        (int X, int Y)[] wallLocations = invertedMap['#'];
        wallLocations = [.. wallLocations.SelectMany<(int X, int Y), (int X, int Y)>(wall => [(wall.X * 2, wall.Y), ((wall.X * 2) + 1, wall.Y)])];

        Crate[] crates = [..invertedMap['O'].Select(location => new Crate(location))];

        ////Console.WriteLine($"Initial state:");
        ////PrintState((map.MaxX * 2) + 1, map.MaxY, robotLocation, wallLocations, crates);

        foreach (string row in instructions)
        {
            foreach (char instruction in row)
            {
                Direction2D direction = Direction2D.ArrowToDirectionMap[instruction];

                if (CanMoveRobot(direction, robotLocation, wallLocations, crates))
                {
                    robotLocation = DoMoveRobot(direction, robotLocation, wallLocations, crates);
                }

                ////Console.WriteLine($"Move {instruction}:");
                ////PrintState((map.MaxX * 2) + 1, map.MaxY, robotLocation, wallLocations, crates);
            }
        }

        return crates.Sum(crate => crate.Score).ToString();
    }

    private static void PrintState(int width, int height, (int X, int Y) robotLocation, (int X, int Y)[] wallLocations, Crate[] crates)
    {
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                (int X, int Y) location = (x, y);
                if (robotLocation == location)
                {
                    Console.Write('@');
                }
                else if (wallLocations.Contains(location))
                {
                    Console.Write('#');
                }
                else if (crates.Any(crate => crate.LeftSideLocation == location))
                {
                    Console.Write("[]");
                    ++x;
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static bool CanMoveRobot(Direction2D direction, (int X, int Y) currentLocation, (int X, int Y)[] wallLocations, Crate[] crates)
    {
        (int X, int Y) destination = direction.GetNextLocation(currentLocation);

        if (wallLocations.Contains(destination))
        {
            return false;
        }

        Crate? crateInLocation = crates.FirstOrDefault(crate => crate.IsInLocation(destination));
        if (crateInLocation is not null)
        {
            return crateInLocation.CanMove(direction, wallLocations, crates);
        }

        // Destination is not a wall or a crate, so...
        return true;
    }

    private static (int X, int Y) DoMoveRobot(Direction2D direction, (int X, int Y) currentLocation, (int X, int Y)[] wallLocations, Crate[] crates)
    {
        (int X, int Y) destination = direction.GetNextLocation(currentLocation);

        Crate? crateInLocation = crates.FirstOrDefault(crate => crate.IsInLocation(destination));
        if (crateInLocation is not null)
        {
            crateInLocation.Move(direction, wallLocations, crates);
        }

        return destination;
    }

    private class Crate((int X, int Y) unexpandedLocation)
    {
        public (int X, int Y) LeftSideLocation { get; set; } = (unexpandedLocation.X * 2, unexpandedLocation.Y);

        public (int X, int Y) RightSideLocation => (this.LeftSideLocation.X + 1, this.LeftSideLocation.Y);

        public int Score => (this.LeftSideLocation.Y * 100) + this.LeftSideLocation.X;

        public bool IsInLocation((int X, int Y) location) => location == this.LeftSideLocation || location == this.RightSideLocation;

        public bool CanMove(Direction2D direction, (int X, int Y)[] wallLocations, Crate[] crates)
        {
            (int X, int Y)[] destinations = [direction.GetNextLocation(this.LeftSideLocation), direction.GetNextLocation(this.RightSideLocation)];

            if (wallLocations.Contains(destinations[0]) || wallLocations.Contains(destinations[1]))
            {
                return false;
            }

            Crate[] cratesInDestination = crates.Where(crate => crate != this && (crate.IsInLocation(destinations[0]) || crate.IsInLocation(destinations[1]))).ToArray();

            if (cratesInDestination.Length > 0)
            {
                return cratesInDestination.All(crate => crate.CanMove(direction, wallLocations, crates));
            }

            return true;
        }

        public void Move(Direction2D direction, (int X, int Y)[] wallLocations, Crate[] crates)
        {
            (int X, int Y)[] destinations = [direction.GetNextLocation(this.LeftSideLocation), direction.GetNextLocation(this.RightSideLocation)];

            Crate[] cratesInDestination = crates.Where(crate => crate != this && (crate.IsInLocation(destinations[0]) || crate.IsInLocation(destinations[1]))).ToArray();

            foreach (Crate crate in cratesInDestination)
            {
                crate.Move(direction, wallLocations, crates);
            }

            this.LeftSideLocation = destinations[0];
        }
    }
}
