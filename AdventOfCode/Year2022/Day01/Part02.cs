namespace AdventOfCode.Year2022.Day01
{
    using System;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Span<int> loads = stackalloc int[1000];
            int written = 0;
            int currentElfLoad = 0;

            foreach (string entry in input)
            {
                if (entry.Length == 0)
                {
                    loads[written++] = currentElfLoad;
                    currentElfLoad = 0;
                }
                else
                {
                    currentElfLoad += int.Parse(entry);
                }
            }

            // The input doesn't end with a double newline, so we've got one final load to add.
            loads[written++] = currentElfLoad;

            loads = loads[..written];

            loads.Sort();

            return (loads[^1] + loads[^2] + loads[^3]).ToString();
        }
    }
}
