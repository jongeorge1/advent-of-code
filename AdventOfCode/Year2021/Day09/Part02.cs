namespace AdventOfCode.Year2021.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private const int Edge = int.MaxValue;
        private const int Unfilled = -1;

        public string Solve(string[] input)
        {
            int[][] rows = input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

            int width = rows[0].Length;
            int height = rows.Length;

            // For this one, we don't much care about the gradients themselves. We know all the basins are bounded by 9s. In my head,
            // I'm thinking of the 9s as delineating the basins, which we can then number by a kind of "flood fill" technique. We can
            // then count the instances of each.
            // Also, it'll be easier for this one to be able to access the grid as a dictionary of points, so...
            // We need to substitute something for the 9, as we'll likely end up using 9 as a basin Id. And we need to represent unfilled
            // areas with something other than 0 to avoid issues with defaults.
            var grid = Enumerable.Range(0, width)
                .SelectMany(x => Enumerable.Range(0, height).Select(y => ((x, y), rows[y][x])))
                .ToDictionary(x => x.Item1, x => x.Item2 == 9 ? Edge : Unfilled);

            int nextBasinId = 1;

            KeyValuePair<(int X, int Y), int> firstUnfilledLocation = grid.FirstOrDefault(x => x.Value == Unfilled);

            while (firstUnfilledLocation.Value != 0)
            {
                FillBasinFrom(firstUnfilledLocation.Key, grid, nextBasinId);

                ++nextBasinId;
                firstUnfilledLocation = grid.FirstOrDefault(x => x.Value == Unfilled);
            }

            // Now we find the biggest basin by grouping up the values and taking the biggest group (that isn't -1).
            int[] threeBiggestBasins = grid.Values.Where(x => x != Edge).GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x).Take(3).ToArray();

            return (threeBiggestBasins[0] * threeBiggestBasins[1] * threeBiggestBasins[2]).ToString();
        }

        private static void FillBasinFrom((int X, int Y) location, Dictionary<(int X, int Y), int> grid, int basinId)
        {
            if (grid.TryGetValue(location, out int current) && current == Unfilled)
            {
                // This location exists and is currently unfilled.
                grid[location] = basinId;
                FillBasinFrom((location.X - 1, location.Y), grid, basinId);
                FillBasinFrom((location.X + 1, location.Y), grid, basinId);
                FillBasinFrom((location.X, location.Y - 1), grid, basinId);
                FillBasinFrom((location.X, location.Y + 1), grid, basinId);
            }
        }
    }
}
