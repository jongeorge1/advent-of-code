namespace AoC.Solutions.Year2021.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var playerStates = new[]
            {
                new DiracDicePlayerState { Position = int.Parse(components[0].Split(' ')[^1]) - 1, Score = 0 },
                new DiracDicePlayerState { Position = int.Parse(components[1].Split(' ')[^1]) - 1, Score = 0 },
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
                var current = queue.Dequeue();

                if (current.IsGameOver())
                {
                    victories[current.Victor()] += current.BranchesRepresented;
                }
                else
                {
                    foreach (var newState in current.TakeTurn())
                    {
                        queue.Enqueue(newState);
                    }
                }
            }

            return victories.Max().ToString();
        }
    }
}
