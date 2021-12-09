namespace AoC.Solutions.Year2021.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        private static readonly char[][] Numbers = new[]
        {
            new char[0],
            new[] { 'c', 'f' },
            new[] { 'a', 'c', 'd', 'e', 'g' },
            new[] { 'a', 'c', 'd', 'f', 'g' },
            new[] { 'b', 'c', 'd', 'f' },
            new[] { 'a', 'b', 'd', 'f', 'g' },
            new[] { 'a', 'b', 'd', 'e', 'f', 'g' },
            new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' },
            new[] { 'a', 'b', 'c', 'd', 'f', 'g' },
        };

        public string Solve(string input)
        {
            // Segment counts:
            // 1 - 2
            // 2 - 5
            // 3 - 5
            // 4 - 4
            // 5 - 5
            // 6 - 6
            // 7 - 3
            // 8 - 7
            // 9 - 6
            // This means the numbers that use unique segment counts are:
            // 1, 4, 7 and 8
            var inputLines = input.Split(Environment.NewLine).ToList();

            (string[] Inputs, string[] Outputs)[] digits = inputLines.Select(x => x.Split('|'))
                .Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .ToArray();

            // Now we need to narrow down the possibilities for each segment
            IEnumerable<int> outputs = digits.Select(x => Decode(x));

            return outputs.Sum().ToString();
        }

        private static int Decode((string[] Inputs, string[] Outputs) display)
        {
            var allCodes = new List<string>(display.Inputs.Union(display.Outputs));

            // We need to identity the candidates for each segment. Then we'll repeatedly
            // iterate until we only have one remaining option for each
            var candidateMappings = new Dictionary<char, List<char>>
            {
                { 'a', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'b', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'c', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'd', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'e', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'f', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
                { 'g', new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            };

            ReduceCandidates(candidateMappings, allCodes.FirstOrDefault(x => x.Length == 2), Numbers[2]);
            ReduceCandidates(candidateMappings, allCodes.FirstOrDefault(x => x.Length == 4), Numbers[4]);
            ReduceCandidates(candidateMappings, allCodes.FirstOrDefault(x => x.Length == 3), Numbers[7]);
            ReduceCandidates(candidateMappings, allCodes.FirstOrDefault(x => x.Length == 7), Numbers[8]);

            return 0;
        }

        private static void ReduceCandidates(Dictionary<char, List<char>> candidateMappings, string? inputs, char[] potentialOutputs)
        {
            if (inputs is not null)
            {
                foreach (char current in inputs)
                {
                    candidateMappings[current] = candidateMappings[current].Intersect(potentialOutputs).ToList();
                }
            }
        }
    }
}
