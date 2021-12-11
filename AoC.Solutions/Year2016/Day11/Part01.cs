namespace AoC.Solutions.Year2016.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var area = new Area(input);
            var queue = new Queue<(Area, int)>();
            queue.Enqueue((area, 0));
            var previousStates = new Dictionary<string, int>();

            while (queue.Count > 0)
            {
                (Area state, int steps) = queue.Dequeue();

                if (state.IsStateFinished)
                {
                    return steps.ToString();
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

            return string.Empty;
        }
    }
}
