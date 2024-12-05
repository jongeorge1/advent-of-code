namespace AdventOfCode.Year2021.Day18
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            SnailfishNumber[] numbers = input.Select(SnailfishNumber.Parse).ToArray();

            SnailfishNumber result = numbers.Aggregate((curr, next) => curr + next);

            return result.Magnitude().ToString();
        }
    }
}
