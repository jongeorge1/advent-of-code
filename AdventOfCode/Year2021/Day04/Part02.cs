namespace AdventOfCode.Year2021.Day04
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] numbers = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            Board[] boards = input[1..].Select(x => new Board(x)).ToArray();

            foreach (int draw in numbers)
            {
                boards.MarkAll(draw);
                Board[] losers = boards.GetLosers();

                if (losers.Length == 0)
                {
                    return boards.Single().Score(draw).ToString();
                }

                boards = losers;
            }

            return string.Empty;
        }
    }
}
