namespace AdventOfCode.Year2023.Day02
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string entry in input)
            {
                Game game = new(entry);
                runningTotal += game.Power();
            }

            return runningTotal.ToString();
        }

        private ref struct Game(ReadOnlySpan<char> input)
        {
            private readonly ReadOnlySpan<char> input = input;

            public int Id { get; set; } = -1;

            public int Power()
            {
                StringExtensions.StringSplitEnumerator entries = this.input.OptimizedSplit(" ".AsSpan());

                // Skip the first entry, it's just "Game"
                entries.MoveNext();
                entries.MoveNext();

                // Extract the Id
                this.Id = int.Parse(entries.Current.Line[0..^1]);

                int maxReds = 0;
                int maxBlues = 0;
                int maxGreens = 0;

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
                            maxReds = Math.Max(count, maxReds);
                            break;
                        case "blue":
                            maxBlues = Math.Max(count, maxBlues);
                            break;
                        case "green":
                            maxGreens = Math.Max(count, maxGreens);
                            break;
                        default:
                            throw new Exception($"Unrecognised colour '{colour}'");
                    }
                }

                return maxReds * maxBlues * maxGreens;
            }
        }
    }
}
