namespace AdventOfCode.Year2021.Day23
{
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string[] additionalRows = new[]
            {
                "DCBA",
                "DBAC",
            };

            var state = new BurrowState(input, additionalRows);
            var processingQueue = new PriorityQueue<BurrowState, long>();
            processingQueue.Enqueue(state, 0);
            var seenStates = new HashSet<string>();

            while (processingQueue.Count > 0)
            {
                BurrowState next = processingQueue.Dequeue();

                string memento = next.GetMemento();
                if (seenStates.Contains(memento))
                {
                    continue;
                }

                seenStates.Add(memento);

                if (next.IsComplete())
                {
                    return next.EnergyConsumed.ToString();
                }

                foreach (BurrowState move in next.PossibleMoves())
                {
                    processingQueue.Enqueue(move, move.EnergyConsumed);
                }
            }

            return string.Empty;
        }
    }
}
