namespace AdventOfCode.Year2020.Day04
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return input
                .Select(x => new Passport(x))
                .Count(x => x.ContainsAllRequiredFields)
                .ToString();
        }
    }
}
