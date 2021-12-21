﻿namespace AoC.Solutions.Year2021.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Scanner[] scanners = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => new Scanner(x)).ToArray();

            Scanner.ResolveAllPositionsRelativeToFirst(scanners);

            // Now we're here, all the scanners should have known positions.
            IEnumerable<(int X, int Y, int Z)> allBeacons = scanners.SelectMany(x => x.GetBeaconsWithAbsolutePositions()).Distinct();

            return allBeacons.Count().ToString();
        }
    }
}
