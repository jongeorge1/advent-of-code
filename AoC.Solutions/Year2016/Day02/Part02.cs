namespace AoC.Solutions.Year2016.Day02
{
    using System;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            char?[][] grid = new[]
            {
                new char?[] { null, null, '1', null, null },
                new char?[] { null, '2', '3', '4', null },
                new char?[] { '5', '6', '7', '8', '9' },
                new char?[] { null, 'A', 'B', 'C', null },
                new char?[] { null, null, 'D', null, null },
            };

            string[] commands = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;

            (int x, int y) location = (0, 2);

            foreach (string command in commands)
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
