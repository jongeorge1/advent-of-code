namespace AoC.Solutions.Year2021.Day18
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            SnailfishNumber[] numbers = input.Split(Environment.NewLine).Select(SnailfishNumber.Parse).ToArray();

            SnailfishNumber result = numbers.Aggregate((curr, next) => curr + next);

            return result.Magnitude().ToString();
        }
    }
}
