namespace AdventOfCode.Year2016.Day11
{
    using System;
    using System.Collections.Generic;

    public static class Day11Common
    {
        public static int GetStepsToBringAllObjectsToFourthFloor(Area area)
        {
            var queue = new Queue<(Area, int)>();
            queue.Enqueue((area, 0));
            var previousStates = new Dictionary<string, int>();

            while (queue.Count > 0)
            {
                (Area state, int steps) = queue.Dequeue();

                if (state.IsStateFinished)
                {
                    return steps;
                }

                string serializedState = state.Serialize();

                if (previousStates.TryGetValue(serializedState, out int previousSteps) && previousSteps <= steps)
                {
                    continue;
                }

                previousStates[serializedState] = steps;

                foreach (Area possibleMove in state.GetPossibleMoves())
                {
                    queue.Enqueue((possibleMove, steps + 1));
                }
            }

            throw new Exception("Unable to solve");
        }
    }
}
