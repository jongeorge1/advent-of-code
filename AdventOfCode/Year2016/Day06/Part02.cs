namespace AdventOfCode.Year2016.Day06
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            return new string(Enumerable.Range(0, input[0].Length).Select(x => input.Select(row => row[x]).GroupBy(x => x).OrderBy(x => x.Count()).First().Key).ToArray());
        }
    }
}
