namespace AdventOfCode.Year2023.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
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
            int validArrangements = 0;

            int springsCount = springs.Length;

            Queue<EvaluationState> evaluations = new ();
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
                    if (TryCreateNewEvaluationState(current, '.', springsCount, expectedGroups, out EvaluationState? newWorkingState))
                    {
                        evaluations.Enqueue(newWorkingState);
                    }

                    if (TryCreateNewEvaluationState(current, '#', springsCount, expectedGroups, out EvaluationState? newBrokenState))
                    {
                        evaluations.Enqueue(newBrokenState);
                    }
                }
                else
                {
                    if (TryCreateNewEvaluationState(
                        current,
                        springs[current.EvaluatedSprings],
                        springsCount,
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
            int expectedSpringsCount,
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

                        return newEvaluationState.CanComplete(expectedSpringsCount, expectedGroups);
                    }

                    return false;
                }

                // We weren't in a group, and we still aren't
                newEvaluationState = new EvaluationState(
                    current.EvaluatedSprings + 1,
                    current.MatchedGroups,
                    0);

                return newEvaluationState.CanComplete(expectedSpringsCount, expectedGroups);
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

            return newEvaluationState.CanComplete(expectedSpringsCount, expectedGroups);
        }

        private record EvaluationState(int EvaluatedSprings, int MatchedGroups, int CurrentGroupSize)
        {
            public bool IsComplete(int expectedSpringsCount)
            {
                return this.EvaluatedSprings == expectedSpringsCount;
            }

            public bool CanComplete(int expectedSpringsCount, int[] expectedGroups)
            {
                int remainingSprings = expectedSpringsCount - this.EvaluatedSprings;
                int[] remainingGroups = expectedGroups[this.MatchedGroups..];
                int minimumRequiredRemainingSprings = remainingGroups.Sum() + remainingGroups.Count() - 1 - this.CurrentGroupSize;
                return remainingSprings >= minimumRequiredRemainingSprings;
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
