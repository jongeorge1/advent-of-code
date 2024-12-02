namespace AdventOfCode.Year2021.Day01
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] depths = input.Select(int.Parse).ToArray();
            return depths.Where((depth, index) => index != 0 && depth > depths[index - 1]).Count().ToString();
        }
    }
}
