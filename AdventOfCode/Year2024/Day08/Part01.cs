namespace AdventOfCode.Year2024.Day08;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        var map = new InvertedMap<char>(Map<char>.CreateCharMap(input, ['.']));

        var antinodeLocations = new HashSet<(int X, int Y)>();

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

                        antinodeLocations.Add((antenna1.X - deltaX, antenna1.Y - deltaY));
                        antinodeLocations.Add((antenna2.X + deltaX, antenna2.Y + deltaY));
                    }
                }
            }
        }

        return antinodeLocations.Count(map.IsLocationInBounds).ToString();
    }
}
