namespace AdventOfCode.Year2024.Day08;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        var map = new InvertedMap<char>(Map<char>.CreateCharMap(input, ['.']));

        var antinodeLocations = new HashSet<(int X, int Y)>();

        int maxX = input[0].Length - 1;
        int maxY = input.Length - 1;

        foreach ((int X, int Y)[] antennaGroup in map.Values)
        {
            foreach ((int X, int Y) antenna1 in antennaGroup)
            {
                foreach ((int X, int Y) antenna2 in antennaGroup)
                {
                    if (antenna1 != antenna2)
                    {
                        int deltaX = antenna2.X - antenna1.X;
                        int deltaY = antenna2.Y - antenna1.Y;

                        int currentX = antenna1.X;
                        int currentY = antenna1.Y;

                        while (map.IsLocationInBounds((currentX, currentY)))
                        {
                            antinodeLocations.Add((currentX, currentY));
                            currentX += deltaX;
                            currentY += deltaY;
                        }

                        currentX = antenna1.X;
                        currentY = antenna1.Y;

                        while (map.IsLocationInBounds((currentX, currentY)))
                        {
                            antinodeLocations.Add((currentX, currentY));
                            currentX -= deltaX;
                            currentY -= deltaY;
                        }
                    }
                }
            }
        }

        return antinodeLocations.Count.ToString();
    }
}
