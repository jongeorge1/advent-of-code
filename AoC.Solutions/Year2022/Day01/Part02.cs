namespace AoC.Solutions.Year2022.Day01
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine + Environment.NewLine)
                .Select(x => x.Split(Environment.NewLine).Select(x => int.Parse(x)).Sum())
                .OrderByDescending(x => x)
                .Take(3)
                .Sum()
                .ToString();
        }
    }
}
