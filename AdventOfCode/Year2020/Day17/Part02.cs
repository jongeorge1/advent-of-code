﻿namespace AdventOfCode.Year2020.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // For some bonkers reason, IDictionary<TKey, TValue> is faster at lookups than a HashSet<T>. So we're using
            // that here, even though we don't care about the values (because we're only storing elements that are "on".
            IDictionary<(int X, int Y, int Z, int W), bool> grid = input
                .Select(x => x.ToCharArray())
                .SelectMany((entry, row) => entry.Select((item, col) => (Location: (x: col, y: row, z: 0, w: 0), Entry: item)))
                .Where(entry => entry.Entry == '#')
                .ToDictionary(item => item.Location, _ => true);

            for (int cycle = 0; cycle < 6; cycle++)
            {
                // Start each cycle by finding the bounds of the current space. For the next cycle, we extend that
                // space out by 1 in each direction then determine the state of each location in that space.
                int xMin = grid.Keys.Min(el => el.X) - 1;
                int xMax = grid.Keys.Max(el => el.X) + 1;
                int yMin = grid.Keys.Min(el => el.Y) - 1;
                int yMax = grid.Keys.Max(el => el.Y) + 1;
                int zMin = grid.Keys.Min(el => el.Z) - 1;
                int zMax = grid.Keys.Max(el => el.Z) + 1;
                int wMin = grid.Keys.Min(el => el.W) - 1;
                int wMax = grid.Keys.Max(el => el.W) + 1;

                var newSpace = new Dictionary<(int X, int Y, int Z, int W), bool>(10000);

                // Now iterate through the space and determine the new state of each cube.
                for (int x = xMin; x <= xMax; x++)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        for (int z = zMin; z <= zMax; z++)
                        {
                            for (int w = wMin; w <= wMax; w++)
                            {
                                IEnumerable<(int X, int Y, int Z, int W)> neighbours = GetNeighbours((x, y, z, w));
                                int activeNeighbours = neighbours.Count(location => grid.ContainsKey(location));
                                if (grid.ContainsKey((x, y, z, w)))
                                {
                                    // It's active...
                                    if (activeNeighbours == 2 || activeNeighbours == 3)
                                    {
                                        newSpace[(x, y, z, w)] = true;
                                    }
                                }
                                else
                                {
                                    // It's inactive
                                    if (activeNeighbours == 3)
                                    {
                                        newSpace[(x, y, z, w)] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                grid = newSpace;
            }

            return grid.Count.ToString();
        }

        private static IEnumerable<(int X, int Y, int Z, int W)> GetNeighbours((int X, int Y, int Z, int W) location)
        {
            for (int x1 = location.X - 1; x1 <= location.X + 1; x1++)
            {
                for (int y1 = location.Y - 1; y1 <= location.Y + 1; y1++)
                {
                    for (int z1 = location.Z - 1; z1 <= location.Z + 1; z1++)
                    {
                        for (int w1 = location.W - 1; w1 <= location.W + 1; w1++)
                        {
                            if ((x1, y1, z1, w1) != location)
                            {
                                yield return (x1, y1, z1, w1);
                            }
                        }
                    }
                }
            }
        }
    }
}
