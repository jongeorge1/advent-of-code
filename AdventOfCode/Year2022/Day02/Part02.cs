namespace AdventOfCode.Year2022.Day02
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine)
                .Sum(Score)
                .ToString();
        }

        private static int Score(string round)
        {
            return round switch
            {
                "A X" => 3,
                "A Y" => 4,
                "A Z" => 8,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 2,
                "C Y" => 6,
                "C Z" => 7,
                _ => throw new Exception("Unexpected round pattern"),
            };
        }
    }
}
