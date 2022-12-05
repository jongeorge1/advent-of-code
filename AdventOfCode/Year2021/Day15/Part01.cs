namespace AdventOfCode.Year2021.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<(int X, int Y), int> grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany((row, y) => row.ToCharArray().Select((col, x) => ((x, y), int.Parse(col.ToString()))))
                .ToDictionary(x => x.Item1, x => x.Item2);

            int maxX = grid.Keys.Max(c => c.X);
            int maxY = grid.Keys.Max(c => c.Y);

            var queue = new PriorityQueue<((int X, int Y) Location, int AccumulatedRisk), int>();
            queue.Enqueue(((0, 0), 0), 0);

            var lowestRisksAtLocations = new Dictionary<(int, int), int>(grid.Count);

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
            return new[]
            {
                (location.X + 1, location.Y),
                (location.X, location.Y + 1),
                (location.X - 1, location.Y),
                (location.X, location.Y - 1),
            };
        }
    }
}
