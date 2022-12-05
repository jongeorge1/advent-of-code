namespace AdventOfCode.Year2022.Day01
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine + Environment.NewLine)
                .Max(row => row.Split(Environment.NewLine).Select(int.Parse).Sum())
                .ToString();
        }
    }
}
