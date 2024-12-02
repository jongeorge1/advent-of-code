namespace AdventOfCode.Year2016.Day18
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // Note for anyone reading. I didn't get around to doing this puzzle until 2021, 5 years after it was originally set.
            // By the time I did, I'd also just got a PC with an AMD Ryzen 9 5950X processor, which I suspect was substantially
            // faster than the Intel i5 processor I had at the time. The point is, it's possible that this was supposed to be one
            // of the problems where you're supposed to calculate rows until you find a pattern you've seen before, at which point
            // you simply stop and derive the answer from what you've already seen. But this solution just brute forces the answer
            // in about 10 seconds.
            const int rows = 400000;
            int width = input[0].Length;

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