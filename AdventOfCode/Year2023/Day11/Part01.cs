namespace AdventOfCode.Year2023.Day11
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
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

                int yExpansion = emptyRows.Count(i => i < y);

                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        int xExpansion = emptyColumns.Count(i => i < x);
                        galaxies.Add(new Galaxy(x + xExpansion, y + yExpansion));
                    }
                }
            }

            int cumulativeDistance = 0;

            // Now calculate the distances. Shortest distance between each galaxy is the Manhattan distance.
            for (int leftIndex = 0; leftIndex < galaxies.Count; leftIndex++)
            {
                for (int rightIndex = leftIndex + 1; rightIndex < galaxies.Count; rightIndex++)
                {
                    Galaxy left = galaxies[leftIndex];
                    Galaxy right = galaxies[rightIndex];
                    int distance = Distance.Manhattan((left.X, left.Y), (right.X, right.Y));
                    cumulativeDistance += distance;
                }
            }

            return cumulativeDistance.ToString();
        }

        private struct Galaxy
        {
            public Galaxy(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }

            public int Y { get; }
        }
    }
}
