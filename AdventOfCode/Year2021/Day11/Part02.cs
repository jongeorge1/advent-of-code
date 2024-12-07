namespace AdventOfCode.Year2021.Day11
{
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
                foreach ((int X, int Y) location in grid.Keys)
                {
                    ++grid[location];
                }

                while (true)
                {
                    (int X, int Y)[] flashers = grid.Where(x => x.Value > 9).Select(x => x.Key).ToArray();

                    if (flashers.Length == 0)
                    {
                        break;
                    }

                    foreach ((int X, int Y) flasher in flashers)
                    {
                        foreach ((int X, int Y) neighbour in this.GetNeighbours(flasher, grid))
                        {
                            ++grid[neighbour];
                        }

                        grid[flasher] = -1;
                        ++flashes;
                    }
                }

                (int X, int Y)[] allFlashedThisStep = grid.Where(x => x.Value == -1).Select(x => x.Key).ToArray();

                if (allFlashedThisStep.Length == 100)
                {
                    return step.ToString();
                }

                foreach ((int X, int Y) flashed in allFlashedThisStep)
                {
                    grid[flashed] = 0;
                }

                ++step;
            }
        }

        private IEnumerable<(int X, int Y)> GetNeighbours((int X, int Y) flasher, Dictionary<(int X, int Y), int> grid)
        {
            for (int x = flasher.X - 1; x <= flasher.X + 1; ++x)
            {
                for (int y = flasher.Y - 1; y <= flasher.Y + 1; ++y)
                {
                    (int X, int Y) location = (x, y);
                    if (location != flasher && grid.TryGetValue(location, out int val) && val != -1)
                    {
                        yield return location;
                    }
                }
            }
        }
    }
}
