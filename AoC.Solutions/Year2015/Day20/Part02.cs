namespace AoC.Solutions.Year2015.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int targetPresents = int.Parse(input);

            // Just iterating until we find the house takes forever. So rather than do that, we'll provision a big array
            // to represent houses, and work out the present counts for each.
            int[] housePresentCounts = new int[targetPresents];

            for (int elf = 1; elf <= housePresentCounts.Length; elf++)
            {
                for (int house = elf; house < Math.Min(housePresentCounts.Length, (elf * 50) + 1); house += elf)
                {
                    housePresentCounts[house] += elf * 11;
                }
            }

            int lowestNumberOfPresentsOverTarget = housePresentCounts.Where(x => x >= targetPresents).First();
            int houseNumber = System.Array.IndexOf(housePresentCounts, lowestNumberOfPresentsOverTarget);

            return houseNumber.ToString();
        }
    }
}