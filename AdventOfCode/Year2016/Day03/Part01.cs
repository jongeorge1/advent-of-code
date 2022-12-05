namespace AdventOfCode.Year2016.Day03
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).OrderBy(x => x).ToArray())
                .Count(x => x[0] + x[1] > x[2])
                .ToString();
        }
    }
}
