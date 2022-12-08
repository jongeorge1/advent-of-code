namespace AdventOfCode.Year2022.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<(int X, int Y), int> forest = input.Split(Environment.NewLine)
                .SelectMany((row, y) => row.Select((col, x) => ((x, y), int.Parse(col.ToString()))))
                .ToDictionary(x => x.Item1, x => x.Item2);

            return forest.Max(x => CalculateScenicScore(x.Key, forest)).ToString();
        }

        private static int CalculateScenicScore((int X, int Y) position, Dictionary<(int X, int Y), int> forest)
        {
            return CountVisibleTrees(position, (0, 1), forest)
                * CountVisibleTrees(position, (0, -1), forest)
                * CountVisibleTrees(position, (1, 0), forest)
                * CountVisibleTrees(position, (-1, 0), forest);
        }

        private static int CountVisibleTrees((int X, int Y) viewPoint, (int X, int Y) direction, Dictionary<(int, int), int> forest)
        {
            int viewPointHeight = forest[viewPoint];
            int visibleTrees = 0;

            (int X, int Y) current = (viewPoint.X + direction.X, viewPoint.Y + direction.Y);

            while (forest.TryGetValue(current, out int height))
            {
                ++visibleTrees;
                if (height >= viewPointHeight)
                {
                    break;
                }

                current = (current.X + direction.X, current.Y + direction.Y);
            }

            return visibleTrees;
        }
    }
}
