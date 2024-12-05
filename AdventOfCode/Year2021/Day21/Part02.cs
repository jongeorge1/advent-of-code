namespace AdventOfCode.Year2021.Day21
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            DiracDicePlayerState[] playerStates = new[]
            {
                new DiracDicePlayerState { Position = int.Parse(input[0].Split(' ')[^1]) - 1, Score = 0 },
                new DiracDicePlayerState { Position = int.Parse(input[1].Split(' ')[^1]) - 1, Score = 0 },
            };

            var gameState = new DiracDiceGameState
            {
                Players = playerStates.ToImmutableList(),
                NextPlayer = 0,
            };

            var queue = new Queue<DiracDiceGameState>();

            queue.Enqueue(gameState);

            long[] victories = new long[] { 0, 0 };

            while (queue.Count > 0)
            {
                DiracDiceGameState current = queue.Dequeue();

                if (current.IsGameOver())
                {
                    victories[current.Victor()] += current.BranchesRepresented;
                }
                else
                {
                    foreach (DiracDiceGameState newState in current.TakeTurn())
                    {
                        queue.Enqueue(newState);
                    }
                }
            }

            return victories.Max().ToString();
        }
    }
}
