namespace AdventOfCode.Year2021.Day14
{
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string current = input[0];

            var transforms = input[2..].ToDictionary(x => x[0..2], x => new[] { new string(new[] { x[0], x[^1] }), new string(new[] { x[^1], x[1] }) });

            var currentPairs = transforms.ToDictionary(x => x.Key, _ => 0L);

            // Break up the input into pairs
            for (int pos = 0; pos < (current.Length - 1); ++pos)
            {
                ++currentPairs[current[pos.. (pos + 2)]];
            }

            for (int step = 0; step < 40; ++step)
            {
                var newPairs = transforms.ToDictionary(x => x.Key, _ => 0L);

                foreach ((string pair, long count) in currentPairs)
                {
                    string[] replacements = transforms[pair];

                    newPairs[replacements[0]] += count;
                    newPairs[replacements[1]] += count;
                }

                currentPairs = newPairs;
            }

            // Now we have to turn this into counts of each letters. At the moment, every character except the last from
            // the original list is double counted, but that's easy to deal with.
            var counts = currentPairs.GroupBy(x => x.Key[0])
                .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

            ++counts[input[0][^1]];

            var orderedCounts = counts.OrderBy(x => x.Value).ToList();

            return (orderedCounts[^1].Value - orderedCounts[0].Value).ToString();
        }
    }
}
