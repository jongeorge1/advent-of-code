namespace AoC.Solutions.Year2016.Day15
{
    using System;
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Disc[] discs = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => new Disc(x)).OrderBy(x => x.Positions).ToArray();

            // We can solve this using Chinese Remainder Theorem. The divisors are our disc position counts - this gives the rotation cycle.
            // The remainders are derived from the disc number, current position, and total positions. Essentially, the remainder we'll be looking
            // for for each disc when we hit the correct time is the number of positions that disc is from 0 at t=0, but then further reduced
            // to take into account the 1 second gap between discs.
            long[] divisors = discs.Select(x => (long)x.Positions).ToArray();
            long[] remainders = discs.Select(x => (long)((x.Positions + x.Positions - x.StartPosition - x.Number) % x.Positions)).ToArray();

            long result = ChineseRemainderTheorem.Solve(divisors, remainders);
            return result.ToString();
        }
    }
}