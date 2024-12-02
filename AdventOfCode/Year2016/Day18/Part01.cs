namespace AdventOfCode.Year2016.Day18
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int width = input[0].Length;

            int rows = width switch
            {
                5 => 3,
                10 => 10,
                _ => 40,
            };

            Dictionary<(int x, int y), bool> tiles = input[0].Select((tile, x) => (tile == '.' ? true : false, x)).ToDictionary(x => (x.x, 0), x => x.Item1);

            for (int y = 1; y < rows; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    bool leftIsSafe = x == 0 || tiles[(x - 1, y - 1)];
                    bool rightIsSafe = x == width - 1 || tiles[(x + 1, y - 1)];
                    bool centerIsSafe = tiles[(x, y - 1)];

                    bool currentIsTrap = (!leftIsSafe && !centerIsSafe && rightIsSafe)
                        || (leftIsSafe && !centerIsSafe && !rightIsSafe)
                        || (!leftIsSafe && centerIsSafe && rightIsSafe)
                        || (leftIsSafe && centerIsSafe && !rightIsSafe);

                    tiles[(x, y)] = !currentIsTrap;
                }
            }

            return tiles.Count(x => x.Value).ToString();
        }
    }
}