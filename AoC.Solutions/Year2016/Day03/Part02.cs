namespace AoC.Solutions.Year2016.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int[][] originalGrid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .ToArray();

            var transformedArray = new List<int[]>();

            // Now we need to pivot the grid.
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < originalGrid.Length; row += 3)
                {
                    transformedArray.Add(new[] { originalGrid[row][col], originalGrid[row + 1][col], originalGrid[row + 2][col] }.OrderBy(x => x).ToArray());
                }
            }

            // Now we can apply the same method as before
            return transformedArray.Count(x => x[0] + x[1] > x[2]).ToString();
        }
    }
}
