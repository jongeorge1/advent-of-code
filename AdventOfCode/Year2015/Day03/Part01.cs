namespace AdventOfCode.Year2015.Day03
{
    using System;
    using System.Collections.Generic;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<char, Func<(int, int), (int, int)>> Mutators =
            new Dictionary<char, Func<(int, int), (int, int)>>
            {
                { '^', input => (input.Item1, input.Item2 + 1) },
                { 'v', input => (input.Item1, input.Item2 - 1) },
                { '>', input => (input.Item1 + 1, input.Item2) },
                { '<', input => (input.Item1 - 1, input.Item2) },
            };

        public string Solve(string[] input)
        {
            var visitedLocations = new HashSet<(int, int)> { (0, 0) };
            (int, int) location = (0, 0);

            foreach (char current in input[0])
            {
                location = Mutators[current](location);
                visitedLocations.Add(location);
            }

            return visitedLocations.Count.ToString();
        }
    }
}
