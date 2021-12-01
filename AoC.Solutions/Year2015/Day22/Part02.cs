namespace AoC.Solutions.Year2015.Day22
{
    using System;
    using System.Collections.Generic;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var initialState = new BattleState
            {
                NextTurnNumber = 1,
                PlayerHitPoints = 50,
                PlayerMana = 500,
                BossHitPoints = int.Parse(rows[0].Split(' ')[^1]),
                BossDamage = int.Parse(rows[1].Split(' ')[^1]),
                HardMode = true,
            };

            // Breadth first search using a priority queue based on number of turns will give us the answer we need.
            var queue = new PriorityQueue<BattleState, int>();

            queue.Enqueue(initialState, initialState.PlayerSpentMana);

            while (queue.Count > 0)
            {
                BattleState current = queue.Dequeue();

                // Is the battle over?
                if (current.IsComplete && current.PlayerWins)
                {
                    Console.WriteLine(string.Join(Environment.NewLine, current.GameLog));

                    return current.PlayerSpentMana.ToString();
                }

                if (!current.IsComplete)
                {
                    foreach (BattleState state in current.TakeTurn())
                    {
                        queue.Enqueue(state, state.PlayerSpentMana);
                    }
                }
            }

            return string.Empty;
        }
    }
}