namespace AoC.Solutions.Year2022.Day03
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine)
                .Chunk(3)
                .Select(x => x[0].First(b => x[1].Contains(b) && x[2].Contains(b)))
                .Sum(b => b <= 'Z' ? b - 38 : b - 96)
                .ToString();
        }
    }
}
