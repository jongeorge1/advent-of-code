namespace AdventOfCode.Year2023.Day11
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            List<Galaxy> galaxies = [];

            List<int> emptyRows = [];
            List<int> emptyColumns = [];

            // Make a list of rows and columns that have no galaxies in them
            for (int y = 0; y < input.Length; y++)
            {
                if (input[y].IndexOf('#') == -1)
                {
                    emptyRows.Add(y);
                }
            }

            for (int x = 0; x < input[0].Length; x++)
            {
                if (!input.Any(row => row[x] == '#'))
                {
                    emptyColumns.Add(x);
                }
            }

            for (int y = 0; y < input.Length; y++)
            {
                if (emptyRows.Contains(y))
                {
                    continue;
                }

                long yExpansion = emptyRows.Count(i => i < y) * 999999;

                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        long xExpansion = emptyColumns.Count(i => i < x) * 999999;
                        galaxies.Add(new Galaxy(x + xExpansion, y + yExpansion));
                    }
                }
            }

            long cumulativeDistance = 0;

            // Now calculate the distances. Shortest distance between each galaxy is the Manhattan distance.
            for (int leftIndex = 0; leftIndex < galaxies.Count; leftIndex++)
            {
                for (int rightIndex = leftIndex + 1; rightIndex < galaxies.Count; rightIndex++)
                {
                    Galaxy left = galaxies[leftIndex];
                    Galaxy right = galaxies[rightIndex];
                    long distance = Distance.Manhattan((left.X, left.Y), (right.X, right.Y));
                    cumulativeDistance += distance;
                }
            }

            return cumulativeDistance.ToString();
        }

        private struct Galaxy
        {
            public Galaxy(long x, long y)
            {
                this.X = x;
                this.Y = y;
            }

            public long X { get; }

            public long Y { get; }
        }
    }
}
