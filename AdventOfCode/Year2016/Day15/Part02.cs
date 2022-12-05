namespace AdventOfCode.Year2016.Day15
{
    using System;
    using System.Linq;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var discs = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => new Disc(x)).OrderBy(x => x.Positions).ToList();
            discs.Add(new Disc(discs.Max(x => x.Number) + 1, 11, 0));

            long[] divisors = discs.Select(x => (long)x.Positions).ToArray();
            long[] remainders = discs.Select(x => (long)((x.Positions + x.Positions - x.StartPosition - x.Number) % x.Positions)).ToArray();

            long result = ChineseRemainderTheorem.Solve(divisors, remainders);
            return result.ToString();
        }
    }
}