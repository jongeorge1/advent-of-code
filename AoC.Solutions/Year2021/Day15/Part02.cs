namespace AoC.Solutions.Year2021.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<(int X, int Y), int> baseGrid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany((row, y) => row.ToCharArray().Select((col, x) => ((x, y), int.Parse(col.ToString()))))
                .ToDictionary(x => x.Item1, x => x.Item2);

            var grid = baseGrid.ToDictionary(x => x.Key, x => x.Value);

            int baseGridWidth = grid.Keys.Max(c => c.X) + 1;
            int baseGridHeight = grid.Keys.Max(c => c.Y) + 1;

            for (int y = 0; y < 5; ++y)
            {
                for (int x = y == 0 ? 1 : 0; x < 5; ++x)
                {
                    foreach (KeyValuePair<(int X, int Y), int> entry in baseGrid)
                    {
                        int newRisk = entry.Value + x + y;
                        if (newRisk > 9)
                        {
                            newRisk -= 9;
                        }

                        grid.Add((entry.Key.X + (x * baseGridWidth), entry.Key.Y + (y * baseGridHeight)), newRisk);
                    }
                }
            }

            int maxX = grid.Keys.Max(c => c.X);
            int maxY = grid.Keys.Max(c => c.Y);

            var queue = new PriorityQueue<((int X, int Y) Location, int AccumulatedRisk), int>();
            queue.Enqueue(((0, 0), 0), 0);

            var lowestRisksAtLocations = new Dictionary<(int, int), int>();

            while (queue.Count > 0)
            {
                ((int X, int Y) location, int currentRisk) = queue.Dequeue();

                if (location == (maxX, maxY))
                {
                    return currentRisk.ToString();
                }

                if (lowestRisksAtLocations.TryGetValue(location, out int previousRiskAtLocation) && previousRiskAtLocation <= currentRisk)
                {
                    continue;
                }
                else
                {
                    lowestRisksAtLocations[location] = currentRisk;
                }

                foreach ((int X, int Y) next in GetNeighbours(location))
                {
                    if (grid.TryGetValue(next, out int nextRisk))
                    {
                        queue.Enqueue((next, currentRisk + nextRisk), currentRisk + nextRisk);
                    }
                }
            }

            return string.Empty;
        }

        private static IEnumerable<(int X, int Y)> GetNeighbours((int X, int Y) location)
        {
            yield return (location.X + 1, location.Y);
            yield return (location.X, location.Y + 1);
            yield return (location.X - 1, location.Y);
            yield return (location.X, location.Y - 1);
        }
    }
}
