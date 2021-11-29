namespace AoC.Solutions.Year2018.Day23
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var bots = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Nanobot(x))
                .ToList();

            Nanobot largestRangeBot = bots.OrderBy(x => x.Radius).Last();

            return bots.Count(x => x.InRangeOf(largestRangeBot)).ToString();
        }
    }
}
