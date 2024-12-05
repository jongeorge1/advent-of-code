namespace AdventOfCode.Year2022.Day18
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        private static readonly (int XOffset, int YOffset, int ZOffset)[] Offsets = new (int, int, int)[]
        {
            (1, 0, 0),
            (-1, 0, 0),
            (0, 1, 0),
            (0, -1, 0),
            (0, 0, -1),
            (0, 0, 1),
        };

        public string Solve(string[] input)
        {
            HashSet<(int X, int Y, int Z)> cubes = [];

            foreach (string current in input)
            {
                int separatorIndex = current.IndexOf(',');
                int x = int.Parse(current[..separatorIndex]);
                ReadOnlySpan<char> remainder = current.AsSpan()[(separatorIndex + 1) ..];
                separatorIndex = remainder.IndexOf(',');
                int y = int.Parse(remainder[..separatorIndex]);
                int z = int.Parse(remainder[(separatorIndex + 1) ..]);

                cubes.Add((x, y, z));
            }

            int exposedSurfaces = 0;

            foreach ((int X, int Y, int Z) cube in cubes)
            {
                foreach ((int XOffset, int YOffset, int ZOffset) offset in Offsets)
                {
                    if (!cubes.Contains((cube.X + offset.XOffset, cube.Y + offset.YOffset, cube.Z + offset.ZOffset)))
                    {
                        ++exposedSurfaces;
                    }
                }
            }

            return exposedSurfaces.ToString();
        }
    }
}
