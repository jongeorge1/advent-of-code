namespace AoC.Solutions.Year2021.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine + Environment.NewLine);
            string current = components[0];

            var transforms = components[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .ToDictionary(x => x[0..2], x => x[^1]);

            for (int step = 0; step < 10; ++step)
            {
                StringBuilder transformed = new (current.Length * 2);

                for (int pos = 0; pos < (current.Length - 1); ++pos)
                {
                    transformed.Append(current[pos]);

                    if (transforms.TryGetValue(current[pos.. (pos + 2)], out char insertion))
                    {
                        transformed.Append(insertion);
                    }
                }

                transformed.Append(current[^1]);

                current = transformed.ToString();
            }

            (char Character, int Occurences)[] elementCounts = current.GroupBy(x => x).Select(x => (x.Key, x.Count())).OrderBy(x => x.Item2).ToArray();

            return (elementCounts[^1].Occurences - elementCounts[0].Occurences).ToString();
        }
    }
}
