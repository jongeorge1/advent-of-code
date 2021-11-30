namespace AoC.Solutions.Year2015.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            BattleState initialState;

            if (input == "test1")
            {
                initialState = new BattleState
                {
                    NextTurnNumber = 1,
                    PlayerHitPoints = 10,
                    PlayerMana = 250,
                    BossDamage = 8,
                    BossHitPoints = 13,
                };
            }
            else if (input == "test2")
            {
                initialState = new BattleState
                {
                    NextTurnNumber = 1,
                    PlayerHitPoints = 10,
                    PlayerMana = 250,
                    BossDamage = 8,
                    BossHitPoints = 14,
                };
            }
            else
            {
                string[] rows = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                initialState = new BattleState
                {
                    NextTurnNumber = 1,
                    PlayerHitPoints = 50,
                    PlayerMana = 500,
                    BossHitPoints = int.Parse(rows[0].Split(' ')[^1]),
                    BossDamage = int.Parse(rows[1].Split(' ')[^1]),
                };
            }

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