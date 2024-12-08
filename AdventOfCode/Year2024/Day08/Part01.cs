﻿namespace AdventOfCode.Year2024.Day08;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        IEnumerable<((int X, int Y) Location, char Frequency)> map = input.SelectMany((row, rowIndex) => row.Select<char, ((int X, int Y) Location, char Frequency)>((col, colIndex) => ((colIndex, rowIndex), col)));
        var antennasByLocation = map.Where(x => x.Frequency != '.').GroupBy(x => x.Frequency).ToDictionary(x => x.Key, x => x.Select(y => y.Location).ToArray());
        var antinodeLocations = new HashSet<(int X, int Y)>();

        int maxX = input[0].Length - 1;
        int maxY = input.Length - 1;

        foreach ((int X, int Y)[] antennaGroup in antennasByLocation.Values)
        {
            foreach ((int X, int Y) antenna1 in antennaGroup)
            {
                foreach ((int X, int Y) antenna2 in antennaGroup)
                {
                    if (antenna1 != antenna2)
                    {
                        int deltaX = antenna2.X - antenna1.X;
                        int deltaY = antenna2.Y - antenna1.Y;

                        antinodeLocations.Add((antenna1.X - deltaX, antenna1.Y - deltaY));
                        antinodeLocations.Add((antenna2.X + deltaX, antenna2.Y + deltaY));
                    }
                }
            }
        }

        return antinodeLocations.Count(location => location.X >= 0 && location.X <= maxX && location.Y >= 0 && location.Y <= maxY).ToString();
    }
}
