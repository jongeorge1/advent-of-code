namespace AdventOfCode.Year2015.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<string, int> KnownFacts = new Dictionary<string, int>
        {
            { "children", 3 },
            { "cats", 7 },
            { "samoyeds", 2 },
            { "pomeranians", 3 },
            { "akitas", 0 },
            { "vizslas", 0 },
            { "goldfish", 5 },
            { "trees", 3 },
            { "cars", 2 },
            { "perfumes", 1 },
        };

        public string Solve(string[] input)
        {
            Sue[] sues = input.Select(x => new Sue(x)).ToArray();

            return sues.First(x => x.Matches(KnownFacts)).Number;
        }
    }
}
