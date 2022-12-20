namespace AdventOfCode.Year2022.Day01
{
    using System;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int currentMaxElfLoad = 0;
            int currentElfLoad = 0;

            foreach (StringExtensions.StringSplitEntry entry in input.OptimizedSplit(Environment.NewLine.AsSpan()))
            {
                if (entry.Line.Length == 0)
                {
                    if (currentElfLoad > currentMaxElfLoad)
                    {
                        currentMaxElfLoad = currentElfLoad;
                    }

                    currentElfLoad = 0;
                }
                else
                {
                    currentElfLoad += int.Parse(entry.Line);
                }
            }

            if (currentElfLoad > currentMaxElfLoad)
            {
                currentMaxElfLoad = currentElfLoad;
            }

            return currentMaxElfLoad.ToString();
        }
    }
}
