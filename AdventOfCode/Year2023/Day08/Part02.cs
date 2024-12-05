namespace AdventOfCode.Year2023.Day08
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string directions = input[0];

            Dictionary<string, (string Left, string Right)> maps = [];

            foreach (string step in input[2..])
            {
                maps.Add(
                    step[0..3],
                    (step[7..10], step[12..15]));
            }

            string[] locations = maps.Keys.Where(x => x.EndsWith('A')).ToArray();

            int steps = 0;

            int position = 0;

            do
            {
                position %= directions.Length;

                for (int i = 0; i < locations.Length; ++i)
                {
                    (string Left, string Right) map = maps[locations[i]];

                    if (directions[position] == 'L')
                    {
                        locations[i] = map.Left;
                    }
                    else
                    {
                        locations[i] = map.Right;
                    }
                }

                ++position;
                ++steps;
            }
            while (locations.Any(x => !x.EndsWith('Z')));

            return steps.ToString();
        }
    }
}
