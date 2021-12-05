namespace AoC.Solutions.Year2016.Day02
{
    using System;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            char[][] grid = new[]
            {
                new[] { '1', '2', '3' },
                new[] { '4', '5', '6' },
                new[] { '7', '8', '9' },
            };

            string[] commands = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;

            (int x, int y) location = (1, 1);

            foreach (string command in commands)
            {
                foreach (char step in command)
                {
                    if (step == 'U' && location.y > 0)
                    {
                        --location.y;
                    }
                    else if (step == 'D' && location.y < 2)
                    {
                        ++location.y;
                    }
                    else if (step == 'L' && location.x > 0)
                    {
                        --location.x;
                    }
                    else if (step == 'R' && location.x < 2)
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
