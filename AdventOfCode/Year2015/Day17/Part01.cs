namespace AdventOfCode.Year2015.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] containers = input.Select(int.Parse).ToArray();
            Array.Sort(containers);
            Array.Reverse(containers);

            // We need to find all the permutations of container sizes that add up to 150 (or 25 if we're in test mode)
            int targetCapacity = containers.Length == 5 ? 25 : 150;
            List<List<int>> availableCombinations = this.FindCombinationsToStoreEggnog(containers, targetCapacity);

            return availableCombinations.Count.ToString();
        }

        private List<List<int>> FindCombinationsToStoreEggnog(int[] containers, int requiredCapacity)
        {
            var possibleCombinations = new List<List<int>>();

            for (int i = 0; i < containers.Length; i++)
            {
                // If the current container exactly fits the required amount, it's a solution in its own right.
                // If it's bigger than the current amount, then we skip it and move on.
                // If it's smaller than the required amount, then it's potentially part of a valid combination.
                if (containers[i] == requiredCapacity)
                {
                    possibleCombinations.Add([containers[i]]);
                }
                else if (containers[i] > requiredCapacity)
                {
                    continue;
                }
                else
                {
                    // We need to see if we can find any combinations that can store the remaining amount.
                    List<List<int>> combinationsForRemainder = this.FindCombinationsToStoreEggnog(containers[(i + 1) ..], requiredCapacity - containers[i]);
                    if (combinationsForRemainder.Count > 0)
                    {
                        // Each of the results is a valid combination and we need to add it to the list of possible combinations
                        // with the current container added to the front of the array.
                        foreach (List<int> combination in combinationsForRemainder)
                        {
                            combination.Add(containers[i]);
                            possibleCombinations.Add(combination);
                        }
                    }
                }
            }

            return possibleCombinations;
        }
    }
}
