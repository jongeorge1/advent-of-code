namespace AdventOfCode.Year2016.Day02
{
    using System;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            char?[][] grid =
            [
                new char?[] { null, null, '1', null, null },
                [null, '2', '3', '4', null],
                ['5', '6', '7', '8', '9'],
                [null, 'A', 'B', 'C', null],
                [null, null, 'D', null, null],
            ];

            string result = string.Empty;

            (int x, int y) location = (0, 2);

            foreach (string command in input)
            {
                foreach (char step in command)
                {
                    if (step == 'U' && location.y > 0 && grid[location.y - 1][location.x].HasValue)
                    {
                        --location.y;
                    }
                    else if (step == 'D' && location.y < 4 && grid[location.y + 1][location.x].HasValue)
                    {
                        ++location.y;
                    }
                    else if (step == 'L' && location.x > 0 && grid[location.y][location.x - 1].HasValue)
                    {
                        --location.x;
                    }
                    else if (step == 'R' && location.x < 4 && grid[location.y][location.x + 1].HasValue)
                    {
                        ++location.x;
                    }
                }

                result += grid[location.y][location.x];
            }

            return result;
        }
    }
}
