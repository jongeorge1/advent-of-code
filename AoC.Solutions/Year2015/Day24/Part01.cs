﻿namespace AoC.Solutions.Year2015.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var weights = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            // We can work out the size of each group by dividing the total size by three.
            long groupWeight = weights.Sum() / 3;

            // We'll assume there isn't a single present of the target size.
            int smallestGroupSize = 2;

            while (true)
            {
                var potentialGroups = weights.GetPermutations(smallestGroupSize).Where(x => x.Sum() == groupWeight).ToList();
                if (potentialGroups.Count > 0)
                {
                    // We have a winner...
                    return potentialGroups.Min(x => x.Product()).ToString();
                }

                ++smallestGroupSize;
            }
        }
    }
}