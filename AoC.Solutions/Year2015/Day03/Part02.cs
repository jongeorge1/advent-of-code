namespace AoC.Solutions.Year2015.Day03
{
    using System;
    using System.Collections.Generic;

    public class Part02 : ISolution
    {
        private static readonly Dictionary<char, Func<(int, int), (int, int)>> Mutators =
            new Dictionary<char, Func<(int, int), (int, int)>>
            {
                { '^', input => (input.Item1, input.Item2 + 1) },
                { 'v', input => (input.Item1, input.Item2 - 1) },
                { '>', input => (input.Item1 + 1, input.Item2) },
                { '<', input => (input.Item1 - 1, input.Item2) },
            };

        public string Solve(string input)
        {
            var visitedLocations = new HashSet<(int, int)> { (0, 0) };
            (int, int) santasLocation = (0, 0);
            (int, int) roboSantasLocation = (0, 0);

            for (int i = 0; i < input.Length; i += 2)
            {
                santasLocation = Mutators[input[i]](santasLocation);
                visitedLocations.Add(santasLocation);
            }

            for (int i = 1; i < input.Length; i += 2)
            {
                roboSantasLocation = Mutators[input[i]](roboSantasLocation);
                visitedLocations.Add(roboSantasLocation);
            }

            return visitedLocations.Count.ToString();
        }
    }
}
