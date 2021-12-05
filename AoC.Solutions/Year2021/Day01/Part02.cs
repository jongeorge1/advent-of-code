namespace AoC.Solutions.Year2021.Day01
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int[] depths = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            return depths.Where((_, i) => i > 2 && depths[i] > depths[i - 3]).Count().ToString();
        }
    }
}
