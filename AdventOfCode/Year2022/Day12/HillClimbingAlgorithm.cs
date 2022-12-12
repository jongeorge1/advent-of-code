namespace AdventOfCode.Year2022.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class HillClimbingAlgorithm
    {
        public HillClimbingAlgorithm(string input)
        {
            this.Map = input.Split(Environment.NewLine)
                .SelectMany((row, y) => row.ToCharArray().Select((col, x) =>
                {
                    if (col == 'S')
                    {
                        this.Start = (x, y);
                        col = 'a';
                    }
                    else if (col == 'E')
                    {
                        this.End = (x, y);
                        col = 'z';
                    }

                    return new KeyValuePair<(int, int), char>((x, y), col);
                })).ToDictionary(x => x.Key, x => x.Value);
        }

        public (int X, int Y) Start { get; private set; }

        public (int X, int Y) End { get; private set; }

        public Dictionary<(int X, int Y), char> Map { get; }

        private record struct State((int X, int Y) Location, int Steps)
        {
        }

        public bool TryFindShortestPathBetween((int X, int Y) start, (int X, int Y) end, [NotNullWhen(true)] out int? steps)
        {
            HashSet<(int X, int Y)> visitedLocations = new();
            Queue<State> states = new();
            states.Enqueue(new State(start, 0));

            while (states.TryDequeue(out State current))
            {
                if (current.Location == end)
                {
                    steps = current.Steps;
                    return true;
                }

                if (visitedLocations.Contains(current.Location))
                {
                    continue;
                }

                visitedLocations.Add(current.Location);

                foreach (State nextLocation in this.GetNextMoves(current))
                {
                    states.Enqueue(nextLocation);
                }
            }

            steps = null;
            return false;
        }

        private static bool CanMoveBetween(char here, char there)
        {
            return (there - here) < 2;
        }

        private IEnumerable<State> GetNextMoves(State current)
        {
            if (this.Map.TryGetValue((current.Location.X - 1, current.Location.Y), out char height) && CanMoveBetween(this.Map[current.Location], height))
            {
                yield return new State((current.Location.X - 1, current.Location.Y), current.Steps + 1);
            }

            if (this.Map.TryGetValue((current.Location.X + 1, current.Location.Y), out height) && CanMoveBetween(this.Map[current.Location], height))
            {
                yield return new State((current.Location.X + 1, current.Location.Y), current.Steps + 1);
            }

            if (this.Map.TryGetValue((current.Location.X, current.Location.Y - 1), out height) && CanMoveBetween(this.Map[current.Location], height))
            {
                yield return new State((current.Location.X, current.Location.Y - 1), current.Steps + 1);
            }

            if (this.Map.TryGetValue((current.Location.X, current.Location.Y + 1), out height) && CanMoveBetween(this.Map[current.Location], height))
            {
                yield return new State((current.Location.X, current.Location.Y + 1), current.Steps + 1);
            }
        }
    }
}
