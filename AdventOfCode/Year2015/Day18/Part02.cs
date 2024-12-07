namespace AdventOfCode.Year2015.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            IDictionary<(int X, int Y), bool> grid = input
                .Select(x => x.ToCharArray())
                .SelectMany((entry, row) => entry.Select((item, col) => (Location: (x: col, y: row), Entry: item)))
                .Where(entry => entry.Entry == '#')
                .ToDictionary(item => item.Location, _ => true);

            int width = input[0].Length;
            int height = input.Length;

            // Ensure the corners are already on.
            grid[(0, 0)] = true;
            grid[(0, height - 1)] = true;
            grid[(width - 1, 0)] = true;
            grid[(width - 1, height - 1)] = true;

            // 100 cycles in "live" mode, 4 in "test" mode.
            int cycles = width == 6 ? 4 : 100;

            for (int cycle = 0; cycle < cycles; cycle++)
            {
                var newGrid = new Dictionary<(int X, int Y), bool>(width * height);

                // Now iterate through the space and determine the new state of each cube.
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Special case: corners are always on:
                        if ((x == 0 && y == 0) || (x == 0 && y == height - 1) || (x == width - 1 && y == 0) || (x == width - 1 && y == height - 1))
                        {
                            newGrid[(x, y)] = true;
                        }
                        else
                        {
                            IEnumerable<(int, int)> neighbours = GetNeighbours((x, y), width, height);
                            int activeNeighbours = neighbours.Count(location => grid.ContainsKey(location));
                            if (grid.ContainsKey((x, y)))
                            {
                                // It's on now...
                                if (activeNeighbours == 2 || activeNeighbours == 3)
                                {
                                    newGrid[(x, y)] = true;
                                }
                            }
                            else
                            {
                                // It's inactive
                                if (activeNeighbours == 3)
                                {
                                    newGrid[(x, y)] = true;
                                }
                            }
                        }
                    }
                }

                grid = newGrid;
            }

            // How many lights are on?
            return grid.Count.ToString();
        }

        private static IEnumerable<(int X, int Y)> GetNeighbours((int X, int Y) location, int width, int height)
        {
            for (int x1 = Math.Max(0, location.X - 1); x1 <= Math.Min(width, location.X + 1); x1++)
            {
                for (int y1 = Math.Max(0, location.Y - 1); y1 <= Math.Min(height, location.Y + 1); y1++)
                {
                    if ((x1, y1) != location)
                    {
                        yield return (x1, y1);
                    }
                }
            }
        }
    }
}