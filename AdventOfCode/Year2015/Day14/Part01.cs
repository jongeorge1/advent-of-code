namespace AdventOfCode.Year2015.Day14
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Reindeer[] reindeer = input.Select(x => new Reindeer(x)).ToArray();
            return reindeer.Max(r => r.GetDistanceAfterTime(2503)).ToString();
        }
    }
}
