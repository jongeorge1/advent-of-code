namespace AdventOfCode.Year2016.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            (long From, long To)[] ipAddresses = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray())
                .Select(x => (x[0], x[1]))
                .OrderBy(x => x.Item1)
                .ToArray();

            long current = 0;
            long allowedIps = 0;

            while (current <= 4294967295)
            {
                // Find any rules that block this IP
                IEnumerable<(long From, long To)> rulesBlockingCurrent = ipAddresses.Where(x => x.From <= current && x.To >= current);

                // If there aren't any, we've found our lowest IP.
                if (!rulesBlockingCurrent.Any())
                {
                    ++allowedIps;
                    ++current;
                }
                else
                {
                    // Otherwise, move on with the first IP that's out of range.
                    current = rulesBlockingCurrent.Max(x => x.To) + 1;
                }
            }

            return allowedIps.ToString();
        }
    }
}