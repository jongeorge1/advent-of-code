namespace AdventOfCode.Year2021.Day19
{
    using System;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Scanner[] scanners = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => new Scanner(x)).ToArray();

            Scanner.ResolveAllPositionsRelativeToFirst(scanners);

            int largestManhattanDistance = 0;

            foreach (Scanner first in scanners)
            {
                foreach (Scanner second in scanners.Where(x => x != first))
                {
                    largestManhattanDistance = Math.Max(largestManhattanDistance, Distance.Manhattan(first.Position!.Value, second.Position!.Value));
                }
            }

            return largestManhattanDistance.ToString();
        }
    }
}
