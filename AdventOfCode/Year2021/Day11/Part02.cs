﻿namespace AdventOfCode.Year2021.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int flashes = 0;

            var grid = input
                .SelectMany((row, y) => row.ToCharArray().Select((c, x) => (x, y, int.Parse(c.ToString()))))
                .ToDictionary(i => (i.x, i.y), i => i.Item3);

            int width = grid.Max(i => i.Key.x + 1);
            int height = grid.Max(i => i.Key.y + 1);

            int step = 1;

            while (true)
            {
                foreach ((int x, int y) location in grid.Keys)
                {
                    ++grid[location];
                }

                while (true)
                {
                    (int x, int y)[] flashers = grid.Where(x => x.Value > 9).Select(x => x.Key).ToArray();

                    if (flashers.Length == 0)
                    {
                        break;
                    }

                    foreach ((int x, int y) flasher in flashers)
                    {
                        foreach ((int, int) neighbour in this.GetNeighbours(flasher, grid))
                        {
                            ++grid[neighbour];
                        }

                        grid[flasher] = -1;
                        ++flashes;
                    }
                }

                (int x, int y)[] allFlashedThisStep = grid.Where(x => x.Value == -1).Select(x => x.Key).ToArray();

                if (allFlashedThisStep.Length == 100)
                {
                    return step.ToString();
                }

                foreach ((int x, int y) flashed in allFlashedThisStep)
                {
                    grid[flashed] = 0;
                }

                ++step;
            }
        }

        private IEnumerable<(int, int)> GetNeighbours((int x, int y) flasher, Dictionary<(int x, int y), int> grid)
        {
            for (int x = flasher.x - 1; x <= flasher.x + 1; ++x)
            {
                for (int y = flasher.y - 1; y <= flasher.y + 1; ++y)
                {
                    (int x, int y) location = (x, y);
                    if (location != flasher && grid.TryGetValue(location, out int val) && val != -1)
                    {
                        yield return location;
                    }
                }
            }
        }
    }
}
