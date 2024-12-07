namespace AdventOfCode.Year2016.Day02
{
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

            (int X, int Y) location = (0, 2);

            foreach (string command in input)
            {
                foreach (char step in command)
                {
                    if (step == 'U' && location.Y > 0 && grid[location.Y - 1][location.X].HasValue)
                    {
                        --location.Y;
                    }
                    else if (step == 'D' && location.Y < 4 && grid[location.Y + 1][location.X].HasValue)
                    {
                        ++location.Y;
                    }
                    else if (step == 'L' && location.X > 0 && grid[location.Y][location.X - 1].HasValue)
                    {
                        --location.X;
                    }
                    else if (step == 'R' && location.X < 4 && grid[location.Y][location.X + 1].HasValue)
                    {
                        ++location.X;
                    }
                }

                result += grid[location.Y][location.X];
            }

            return result;
        }
    }
}
