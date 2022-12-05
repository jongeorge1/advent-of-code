namespace AdventOfCode.Year2015.Day06
{
    using System;
    using System.Text.RegularExpressions;

    public class Part02 : ISolution
    {
        private static readonly Regex ExtractionRegex = new Regex(@"((?:turn on)|(?:turn off)|(?:toggle)) (\d+),(\d+) through (\d+),(\d+)", RegexOptions.Compiled);

        private readonly int[,] grid = new int[1000, 1000];

        public string Solve(string input)
        {
            MatchCollection matches = ExtractionRegex.Matches(input);

            foreach (Match match in matches)
            {
                int fromX = int.Parse(match.Groups[2].Value);
                int fromY = int.Parse(match.Groups[3].Value);
                int toX = int.Parse(match.Groups[4].Value);
                int toY = int.Parse(match.Groups[5].Value);

                switch (match.Groups[1].Value)
                {
                    case "turn on":
                        this.IncreaseBrightness(fromX, fromY, toX, toY);
                        break;

                    case "turn off":
                        this.DecreaseBrightness(fromX, fromY, toX, toY);
                        break;

                    case "toggle":
                        this.IncreaseBrightnessBy2(fromX, fromY, toX, toY);
                        break;
                }
            }

            int count = 0;
            foreach (int current in this.grid)
            {
                count += current;
            }

            return count.ToString();
        }

        private void IncreaseBrightness(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, x => ++x);
        }

        private void DecreaseBrightness(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, x => x == 0 ? 0 : x - 1);
        }

        private void IncreaseBrightnessBy2(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, x => x += 2);
        }

        private void Set(int fromX, int fromY, int toX, int toY, Func<int, int> setter)
        {
            for (int x = fromX; x <= toX; x++)
            {
                for (int y = fromY; y <= toY; y++)
                {
                    this.grid[x, y] = setter(this.grid[x, y]);
                }
            }
        }
    }
}
