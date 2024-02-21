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
            int validArrangements = 0;

            int springsCount = springs.Length;

            Queue<EvaluationState> evaluations = new ();
            evaluations.Enqueue(new EvaluationState(new List<char>(), 0));

            while (evaluations.TryDequeue(out var current))
            {
                if (current.SpringsIndex == springs.Length)
                {
                    // We have reached the end and this is a valid arrangement.
                    ++validArrangements;
                    continue;
                }

                if (springs[current.SpringsIndex] == '?')
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
                        springs[current.SpringsIndex],
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
            int springsCount,
            int[] expectedGroups,
            out EvaluationState newEvaluationState)
        {
            newEvaluationState = new EvaluationState(new List<char>(current.Springs), current.SpringsIndex + 1);
            newEvaluationState.Springs.Add(newSpringState);
            return IsValid(newEvaluationState, springsCount, expectedGroups);
        }

        private static bool IsValid(EvaluationState state, int totalSpings, int[] expectedGroups)
        {
            int checksumPointer = 0;
            bool inGroup = false;
            int currentGroupSize = 0;

            for (int i = 0; i < state.Springs.Count; ++i)
            {
                if (state.Springs[i] == '#')
                {
                    // We've entered a new group. This is fine as long as we're expecting another group...
                    if (checksumPointer == expectedGroups.Length)
                    {
                        return false;
                    }

                    inGroup = true;
                    ++currentGroupSize;
                }
                else if (inGroup)
                {
                    // We've just finished a group
                    if (currentGroupSize == expectedGroups[checksumPointer])
                    {
                        // The group was the expected size. That means we're still potentially valid.
                        // Set up to evaluate the next group.
                        inGroup = false;
                        currentGroupSize = 0;
                        checksumPointer++;
                    }
                    else
                    {
                        // We've just finished a group that didn't match the expected size. We're not valid.
                        return false;
                    }
                }
            }

            // If we were in a group at the end of the loop, we need to do the final "end of group" check.
            if (inGroup)
            {
                if (currentGroupSize == expectedGroups[checksumPointer])
                {
                    checksumPointer++;
                }
                else if (currentGroupSize < expectedGroups[checksumPointer] && state.Springs.Count < totalSpings)
                {
                    // We haven't got the full picture yet, so although the final group size doesn't match,
                    // this is still potentially valid.
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // We're now potentially valid. However, if we're at the end of the list of springs but not
            // the end of the checksums we've got a problem...
            if (state.Springs.Count == totalSpings && checksumPointer != expectedGroups.Length)
            {
                return false;
            }

            return true;
        }

        private record EvaluationState(List<char> Springs, int SpringsIndex)
        {
        }
    }
}
