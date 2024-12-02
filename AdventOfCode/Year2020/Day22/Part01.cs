namespace AdventOfCode.Year2020.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int blankLineIndex = Array.IndexOf(input, string.Empty);
            IEnumerable<int>? deck1Input = input[1..blankLineIndex].Select(int.Parse);
            IEnumerable<int>? deck2Input = input[(blankLineIndex + 2) ..].Select(int.Parse);

            var deck1 = new Queue<int>(deck1Input);
            var deck2 = new Queue<int>(deck2Input);

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                int player1 = deck1.Dequeue();
                int player2 = deck2.Dequeue();

                if (player1 > player2)
                {
                    deck1.Enqueue(player1);
                    deck1.Enqueue(player2);
                }
                else
                {
                    deck2.Enqueue(player2);
                    deck2.Enqueue(player1);
                }
            }

            Queue<int> winner = deck1.Count == 0 ? deck2 : deck1;
            int multiplier = winner.Count;
            int total = 0;

            while (winner.Count > 0)
            {
                total += winner.Dequeue() * multiplier--;
            }

            return total.ToString();
        }
    }
}
