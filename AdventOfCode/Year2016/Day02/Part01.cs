namespace AdventOfCode.Year2016.Day02
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            char[][] grid =
            [
                ['1', '2', '3'],
                ['4', '5', '6'],
                ['7', '8', '9'],
            ];

            string result = string.Empty;

            (int X, int Y) location = (1, 1);

            foreach (string command in input)
            {
                foreach (char step in command)
                {
                    if (step == 'U' && location.Y > 0)
                    {
                        --location.Y;
                    }
                    else if (step == 'D' && location.Y < 2)
                    {
                        ++location.Y;
                    }
                    else if (step == 'L' && location.X > 0)
                    {
                        --location.X;
                    }
                    else if (step == 'R' && location.X < 2)
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
