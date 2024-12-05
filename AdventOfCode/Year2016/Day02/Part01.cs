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

            (int x, int y) location = (1, 1);

            foreach (string command in input)
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
