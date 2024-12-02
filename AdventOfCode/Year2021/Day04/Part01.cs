namespace AdventOfCode.Year2021.Day04
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] numbers = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            Board[] boards = input[1..].Select(x => new Board(x)).ToArray();

            foreach (int draw in numbers)
            {
                boards.MarkAll(draw);
                Board? winner = boards.GetWinner();
                if (winner is not null)
                {
                    return winner.Score(draw).ToString();
                }
            }

            return string.Empty;
        }
    }
}
