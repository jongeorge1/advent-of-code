namespace AdventOfCode.Year2015.Day20
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int targetPresents = int.Parse(input[0]);

            // Just iterating until we find the house takes forever. So rather than do that, we'll provision a big array
            // to represent houses, and work out the present counts for each.
            int[] housePresentCounts = new int[targetPresents];

            for (int elf = 1; elf <= housePresentCounts.Length; elf++)
            {
                for (int house = elf; house < housePresentCounts.Length; house += elf)
                {
                    housePresentCounts[house] += elf * 10;
                }
            }

            int lowestNumberOfPresentsOverTarget = housePresentCounts.Where(x => x >= targetPresents).First();
            int houseNumber = System.Array.IndexOf(housePresentCounts, lowestNumberOfPresentsOverTarget);

            return houseNumber.ToString();
        }
    }
}