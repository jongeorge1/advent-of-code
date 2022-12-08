namespace AdventOfCode.Year2022.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine);

            Dictionary<(int X, int Y), int> forest = rows
                .SelectMany((row, y) => row.Select((col, x) => ((x, y), int.Parse(col.ToString()))))
                .ToDictionary(x => x.Item1, x => x.Item2);

            int height = rows.Length;
            int width = rows[0].Length;

            // Need to "look" at the forest from every point around the outside and build up a list.
            // of visible trees. Use a hashset to avoid the need to check whether we've seen a tree
            // before; adding the same item multiple times does not result in multiple instances
            // in the set.
            HashSet<(int X, int Y)> visibleTrees = new();

            // Look from every point at the "top" and "bottom" of the forest.
            for (int x = 0; x < width; ++x)
            {
                AddTreesVisibleFrom((x, 0), (0, 1), forest, visibleTrees);
                AddTreesVisibleFrom((x, height - 1), (0, -1), forest, visibleTrees);
            }

            // Look from every point at the "left" and "right" of the forest.
            for (int y = 0; y < height; ++y)
            {
                AddTreesVisibleFrom((0, y), (1, 0), forest, visibleTrees);
                AddTreesVisibleFrom((width - 1, y), (-1, 0), forest, visibleTrees);
            }

            return visibleTrees.Count().ToString();
        }

        private static void AddTreesVisibleFrom((int X, int Y) start, (int X, int Y) direction, Dictionary<(int, int), int> forest, HashSet<(int, int)> visibleTrees)
        {
            int maxHeightFromThisDirection = -1;
            (int X, int Y) current = start;

            while (forest.TryGetValue(current, out int height))
            {
                if (height > maxHeightFromThisDirection)
                {
                    visibleTrees.Add(current);
                    maxHeightFromThisDirection = height;
                }

                current = (current.X + direction.X, current.Y + direction.Y);
            }
        }
    }
}
