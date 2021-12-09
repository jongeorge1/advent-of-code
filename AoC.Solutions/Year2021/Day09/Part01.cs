namespace AoC.Solutions.Year2021.Day09
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int[][] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

            int width = rows[0].Length;
            int height = rows.Length;

            int totalRisk = 0;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (x > 0 && rows[y][x - 1] <= rows[y][x])
                    {
                        continue;
                    }

                    if (x < (width - 1) && rows[y][x + 1] <= rows[y][x])
                    {
                        continue;
                    }

                    if (y > 0 && rows[y - 1][x] <= rows[y][x])
                    {
                        continue;
                    }

                    if (y < (height - 1) && rows[y + 1][x] <= rows[y][x])
                    {
                        continue;
                    }

                    // We're at a low point.
                    totalRisk += rows[y][x] + 1;
                }
            }

            return totalRisk.ToString();
        }

    }
}
