namespace AdventOfCode.Year2023.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private static readonly Dictionary<char, int> CardValues = new Dictionary<char, int>()
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            { 'J', 1 },
            { 'T', 10 },
            { '9', 9 },
            { '8', 8 },
            { '7', 7 },
            { '6', 6 },
            { '5', 5 },
            { '4', 4 },
            { '3', 3 },
            { '2', 2 },
        };

        public string Solve(string[] input)
        {
            IEnumerable<Hand> hands = input.Select(Hand.Create).Order();

            int rank = 1;
            int winnings = 0;
            foreach (Hand hand in hands)
            {
                winnings += hand.Bid * rank;
                ++rank;
            }

            return winnings.ToString();
        }

        public struct Hand : IComparable<Hand>
        {
            public static Hand Create(string input)
            {
                char[] cards = input[0..5].ToCharArray();
                int bid = int.Parse(input[6..]);

                IEnumerable<char> nonJokers = cards.Where(x => x != 'J');
                int jokerCount = 5 - nonJokers.Count();

                int strength = 0;

                if (jokerCount == 5)
                {
                    strength = 7;
                }
                else
                {
                    (char Card, int Count)[] groupedCards = nonJokers.GroupBy(x => x).Select(x => (x.Key, x.Count())).OrderByDescending(x => x.Item2).ToArray();

                    if (groupedCards.Length == 1)
                    {
                        strength = 7; // 5 of a kind
                    }
                    else if (groupedCards.Length == 2 && groupedCards[0].Count + jokerCount == 4)
                    {
                        strength = 6; // 4 of a kind
                    }
                    else if (groupedCards.Length == 2)
                    {
                        strength = 5; // Full house
                    }
                    else if (groupedCards.Length == 3 && groupedCards[0].Count + jokerCount == 3)
                    {
                        strength = 4; // 3 of a kind
                    }
                    else if (groupedCards.Length == 3)
                    {
                        strength = 3; // 2 pair
                    }
                    else if (groupedCards.Length == 4)
                    {
                        strength = 2; // 1 pair
                    }
                    else
                    {
                        strength = 1; // High card
                    }
                }

                return new Hand
                {
                    Cards = cards,
                    Type = strength,
                    Bid = bid,
                };
            }

            public char[] Cards { get; set; }

            public int Bid { get; set; }

            public int Type { get; set; }

            public int CompareTo(Hand other)
            {
                if (this.Type != other.Type)
                {
                    return this.Type - other.Type;
                }

                // Same rank; fall back to card strength
                for (int card = 0; card < this.Cards.Length; ++card)
                {
                    int thisStrength = CardValues[this.Cards[card]];
                    int thatStrength = CardValues[other.Cards[card]];
                    if (thisStrength != thatStrength)
                    {
                        return thisStrength - thatStrength;
                    }
                }

                return 0;
            }
        }
    }
}
