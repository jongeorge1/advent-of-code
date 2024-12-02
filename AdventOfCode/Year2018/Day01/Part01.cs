namespace AdventOfCode.Year2018.Day01
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return input.Select(x => int.Parse(x)).Sum().ToString();
        }
    }
}
