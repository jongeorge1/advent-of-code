namespace AdventOfCode.Year2023.Day18;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        IEnumerable<Instruction> instructions = input.Select(x => new Instruction(x));

        (int X, int Y) currentLocation = (0, 0);
        HashSet<(int X, int Y)> holeLocations = [currentLocation];

        foreach (Instruction instruction in instructions)
        {
            for (int i = 0; i < instruction.Distance; i++)
            {
                currentLocation = instruction.Direction.GetNextLocation(currentLocation);
                holeLocations.Add(currentLocation);
            }
        }

        // Now we need to dig out the interior. First let's find the bounds of our hole.
        int minX = holeLocations.Min(x => x.X);
        int maxX = holeLocations.Max(x => x.X);
        int minY = holeLocations.Min(x => x.Y);
        int maxY = holeLocations.Max(x => x.Y);

        PrintHole(holeLocations, minX, maxX, minY, maxY);

        // From a quick inspection of the boundaries defined by our input data, there are some challenges with this:
        // 1. The hole is a fairly random shape. In some cases, an area of hole might already have a dug line straight through it.
        //    This means that winding count won't work.
        return string.Empty;
    }

    private static void PrintHole(HashSet<(int X, int Y)> holeLocations, int minX, int maxX, int minY, int maxY)
    {
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if ((x, y) == (0, 0))
                {
                    Console.Write('S');
                }
                else if (holeLocations.Contains((x, y)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }

    public readonly struct Instruction(string input)
    {
        public Direction2D Direction { get; } = Direction2D.UpDownLeftRightToDirectionMap[input[0]];

        public int Distance { get; } = input[2] - '0';

        public string Colour { get; } = input[5..12];
    }
}
