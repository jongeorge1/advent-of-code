namespace AoC.Solutions.Year2015.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            long[] weights = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).OrderByDescending(x => x).ToArray();

            // We can work out the size of each group by dividing the total size by three.
            long groupWeight = weights.Sum() / 3;

            // We'll assume there isn't a single present of the target size.
            int targetGroupSize = 2;

            while (true)
            {
                ////List<List<long>> potentialGroups = GetPotentialGroupsOfSize(weights, groupWeight, targetGroupSize);
                var potentialGroups = weights.GetPermutations(targetGroupSize).Where(x => x.Sum() == groupWeight).ToList();
                if (potentialGroups.Count > 0)
                {
                    // We have a winner...
                    return potentialGroups.Min(x => x.Product()).ToString();
                }

                ++targetGroupSize;
            }
        }

        private static List<List<long>> GetPotentialGroupsOfSize(long[] weights, long targetWeight, int targetGroupSize)
        {
            var results = new List<List<long>>();

            if (targetGroupSize == 1)
            {
                if (weights.Contains(targetWeight))
                {
                    results.Add(new List<long> { targetWeight });
                }

                return results;
            }

            // We'll go through looking for all possible combinations...
            for (int index = 0; index < weights.Length - targetGroupSize; ++index)
            {
                // To find the possible combinations that include the item at the current index, we look for
                // possible combinations out of the remaining items that meet the target weight.
                List<List<long>> combinationsForRemainingWeight = GetPotentialGroupsOfSize(weights[(index + 1) ..], targetWeight - weights[index], targetGroupSize - 1);

                foreach (List<long> combination in combinationsForRemainingWeight)
                {
                    combination.Insert(0, weights[0]);
                }

                results.AddRange(combinationsForRemainingWeight);
            }

            return results;
        }
    }
}