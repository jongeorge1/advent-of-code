namespace AdventOfCode.Year2023.Day12
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string item in input)
            {
                runningTotal += GetPossibleArrangementCount(item);
            }

            return runningTotal.ToString();
        }

        private static int GetPossibleArrangementCount(string item)
        {
            // Split into the spring map and checksums
            int spacePosition = item.IndexOf(' ');
            string springs = item[..spacePosition];
            int[] expectedGroups = item[(spacePosition + 1) ..].Split(',').Select(int.Parse).ToArray();

            Dictionary<int, int> seenStates = [];
            return GetPossibleArrangementCountFromPosition(springs, 0, expectedGroups, seenStates);
        }

        private static int GetPossibleArrangementCountFromPosition(string map, int position, int[] remainingGroupsToMatch, Dictionary<int, int> seenStates)
        {
            int possibleCombinations = 0;

            // Have we been in this position before? We need a key that captures both position and remaining group count.
            int key = (position << 8) + remainingGroupsToMatch.Length;
            if (seenStates.TryGetValue(key, out int storedCombinations))
            {
                return storedCombinations;
            }

            // Are there remaining groups to match?
            if (remainingGroupsToMatch.Length == 0)
            {
                // This only works if there are no more broken springs
                if (position >= map.Length || map.IndexOf('#', position) == -1)
                {
                    seenStates.Add(key, 1);
                    return 1;
                }

                // If we're here, it doesn't work
                seenStates.Add(key, 0);
                return 0;
            }

            // Have we reached the end?
            if (position >= map.Length)
            {
                // We must still have groups to match, otherwise we'd have gone through the conditional block above. So
                // this doesn't work.
                return 0;
            }

            // Now see what type of space we're on. If it's a . (or if it could be a .), we'll move on a step and stash the results.
            if (map[position] == '.' || map[position] == '?')
            {
                possibleCombinations += GetPossibleArrangementCountFromPosition(map, position + 1, remainingGroupsToMatch, seenStates);
            }

            // If it's a '#' or it could be a '#' then we're starting a new group. Have a look at the expected next
            // group size and work out if that's possible.
            bool canMakeNextGroup = true;
            if (map[position] == '#' || map[position] == '?')
            {
                int expectedNextGroupSize = remainingGroupsToMatch[0];
                if (position + expectedNextGroupSize > map.Length)
                {
                    canMakeNextGroup = false;
                }
                else
                {
                    for (int i = position + 1; i < position + expectedNextGroupSize; ++i)
                    {
                        if (map[i] == '.')
                        {
                            canMakeNextGroup = false;
                            break;
                        }
                    }
                }

                // We can successfully make the next group. We now just need to check that the next character is not a '#'
                if (canMakeNextGroup && (position + expectedNextGroupSize < map.Length) && map[position + expectedNextGroupSize] == '#')
                {
                    canMakeNextGroup = false;
                }

                if (canMakeNextGroup)
                {
                    // See what comes next
                    possibleCombinations += GetPossibleArrangementCountFromPosition(map, position + expectedNextGroupSize + 1, remainingGroupsToMatch[1..], seenStates);
                }
            }

            seenStates.Add(key, possibleCombinations);
            return possibleCombinations;
        }
    }
}
