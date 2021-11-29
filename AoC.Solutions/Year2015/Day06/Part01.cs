namespace AoC.Solutions.Year2015.Day06
{
    using System;
    using System.Text.RegularExpressions;

    public class Part01 : ISolution
    {
        private static readonly Regex ExtractionRegex = new Regex(@"((?:turn on)|(?:turn off)|(?:toggle)) (\d+),(\d+) through (\d+),(\d+)", RegexOptions.Compiled);

        private readonly bool[,] grid = new bool[1000, 1000];

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
                        this.TurnOn(fromX, fromY, toX, toY);
                        break;

                    case "turn off":
                        this.TurnOff(fromX, fromY, toX, toY);
                        break;

                    case "toggle":
                        this.Toggle(fromX, fromY, toX, toY);
                        break;
                }
            }

            int count = 0;
            foreach (bool current in this.grid)
            {
                if (current)
                {
                    ++count;
                }
            }

            return count.ToString();
        }

        private void TurnOn(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, _ => true);
        }

        private void TurnOff(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, _ => false);
        }

        private void Toggle(int fromX, int fromY, int toX, int toY)
        {
            this.Set(fromX, fromY, toX, toY, x => !x);
        }

        private void Set(int fromX, int fromY, int toX, int toY, Func<bool, bool> setter)
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
