namespace AdventOfCode.Year2023.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string item in input)
            {
                runningTotal += GetPossibleArrangementCount(item);
                Console.WriteLine(runningTotal);
            }

            return runningTotal.ToString();
        }

        private static int GetPossibleArrangementCount(string item)
        {
            // Split into the spring map and checksums
            int spacePosition = item.IndexOf(' ');
            string springs = item[..spacePosition];

            // Expand the springs
            springs = string.Concat(springs, "?", springs, "?", springs, "?", springs, "?", springs);

            // Expand the groups
            int[] expectedGroups = item[(spacePosition + 1)..].Split(',').Select(int.Parse).ToArray();
            var expandedGroups = new List<int>(expectedGroups);
            expandedGroups.AddRange(expectedGroups);
            expandedGroups.AddRange(expectedGroups);
            expandedGroups.AddRange(expectedGroups);
            expandedGroups.AddRange(expectedGroups);

            expectedGroups = expandedGroups.ToArray();
            int validArrangements = 0;

            int springsCount = springs.Length;

            Queue<EvaluationState> evaluations = new();
            evaluations.Enqueue(new EvaluationState(0, 0, 0));

            while (evaluations.TryDequeue(out EvaluationState? current))
            {
                if (current.IsComplete(springsCount))
                {
                    if (current.ExpectedGroupsFound(expectedGroups))
                    {
                        ++validArrangements;
                    }

                    continue;
                }

                if (springs[current.EvaluatedSprings] == '?')
                {
                    if (TryCreateNewEvaluationState(current, '.', expectedGroups, out EvaluationState? newWorkingState))
                    {
                        evaluations.Enqueue(newWorkingState);
                    }

                    if (TryCreateNewEvaluationState(current, '#', expectedGroups, out EvaluationState? newBrokenState))
                    {
                        evaluations.Enqueue(newBrokenState);
                    }
                }
                else
                {
                    if (TryCreateNewEvaluationState(
                        current,
                        springs[current.EvaluatedSprings],
                        expectedGroups,
                        out EvaluationState? newState))
                    {
                        evaluations.Enqueue(newState);
                    }
                }
            }

            return validArrangements;
        }

        private static bool TryCreateNewEvaluationState(
            EvaluationState current,
            char newSpringState,
            int[] expectedGroups,
            [NotNullWhen(true)] out EvaluationState? newEvaluationState)
        {
            newEvaluationState = null;

            if (newSpringState == '.')
            {
                if (current.CurrentGroupSize > 0)
                {
                    // We've just finished off a group. Compare it against the expected next group size
                    // and return a new state if it's correct.
                    if (current.CurrentGroupSize == expectedGroups[current.MatchedGroups])
                    {
                        // All good. Return a new state
                        newEvaluationState = new EvaluationState(
                            current.EvaluatedSprings + 1,
                            current.MatchedGroups + 1,
                            0);
                        return true;
                    }

                    return false;
                }

                // We weren't in a group, and we still aren't
                newEvaluationState = new EvaluationState(
                    current.EvaluatedSprings + 1,
                    current.MatchedGroups,
                    0);

                return true;
            }

            // We're in a group. If we weren't in one already, we should check to see that we're expecting
            // another group
            if (current.CurrentGroupSize == 0 && current.MatchedGroups == expectedGroups.Length)
            {
                return false;
            }

            // If we are in one, we should check to see that we haven't exceeded the next expected group size.
            // (Use > because we are about to add one to the current group size.
            if (current.CurrentGroupSize >= expectedGroups[current.MatchedGroups])
            {
                return false;
            }

            // We're in a group. We don't need to do anything special here, because we'll evaluate
            // whether things are correct once we've exited the group.
            newEvaluationState = new EvaluationState(
                current.EvaluatedSprings + 1,
                current.MatchedGroups,
                current.CurrentGroupSize + 1);

            return true;
        }

        private record EvaluationState(int EvaluatedSprings, int MatchedGroups, int? CurrentGroupSize)
        {
            public bool IsComplete(int expectedSpringsCount)
            {
                return this.EvaluatedSprings == expectedSpringsCount;
            }

            public bool ExpectedGroupsFound(int[] expectedGroups)
            {
                int matchedGroups = this.MatchedGroups;

                if (this.CurrentGroupSize > 0)
                {
                    // We were in a group when we hit the end. If there's exactly one group left to match,
                    // and it's the right size, then we're good. Otherwise not.
                    if (expectedGroups[this.MatchedGroups] == this.CurrentGroupSize)
                    {
                        matchedGroups++;
                    }
                    else
                    {
                        // The group size didn't match, so this can't be valid.
                        return false;
                    }
                }

                return matchedGroups == expectedGroups.Length;
            }
        }
    }
}
