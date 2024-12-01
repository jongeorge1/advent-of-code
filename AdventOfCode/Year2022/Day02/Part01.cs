namespace AdventOfCode.Year2022.Day02
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return input
                .Sum(Score)
                .ToString();
        }

        private static int Score(string round)
        {
            return round switch
            {
                "A X" => 4,
                "B Y" => 5,
                "C Z" => 6,
                "A Y" => 8,
                "B Z" => 9,
                "C X" => 7,
                "A Z" => 3,
                "B X" => 1,
                "C Y" => 2,
                _ => throw new Exception("Unexpected round pattern"),
            };
        }
    }
}
