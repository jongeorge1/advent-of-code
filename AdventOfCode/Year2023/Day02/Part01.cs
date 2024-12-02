namespace AdventOfCode.Year2023.Day02
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string entry in input)
            {
                Game game = new(entry);
                if (game.IsPossible())
                {
                    runningTotal += game.Id;
                }
            }

            return runningTotal.ToString();
        }

        private ref struct Game(ReadOnlySpan<char> input)
        {
            private readonly ReadOnlySpan<char> input = input;

            public int Id { get; set; } = -1;

            public bool IsPossible()
            {
                StringExtensions.StringSplitEnumerator entries = this.input.OptimizedSplit(" ".AsSpan());

                // Skip the first entry, it's just "Game"
                entries.MoveNext();
                entries.MoveNext();

                // Extract the Id
                this.Id = int.Parse(entries.Current.Line[0..^1]);

                const int MaxRed = 12;
                const int MaxBlue = 14;
                const int MaxGreen = 13;

                while (entries.MoveNext())
                {
                    int count = int.Parse(entries.Current.Line);

                    entries.MoveNext();

                    ReadOnlySpan<char> colour = entries.Current.Line[^1] == ',' || entries.Current.Line[^1] == ';'
                        ? entries.Current.Line[0..^1]
                        : entries.Current.Line;

                    switch (colour)
                    {
                        case "red":
                            if (count > MaxRed)
                            {
                                return false;
                            }

                            break;
                        case "blue":
                            if (count > MaxBlue)
                            {
                                return false;
                            }

                            break;
                        case "green":
                            if (count > MaxGreen)
                            {
                                return false;
                            }

                            break;
                        default:
                            throw new Exception("Unrecognised colour");
                    }
                }

                return true;
            }
        }
    }
}
