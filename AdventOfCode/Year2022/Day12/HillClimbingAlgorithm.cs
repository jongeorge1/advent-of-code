namespace AdventOfCode.Year2022.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;

    public ref struct HillClimbingAlgorithm
    {
        public HillClimbingAlgorithm(string[] input)
        {
            this.Map = [];

            this.MapHeight = input.Length;
            this.MapWidth = input[0].Length;

            for (int y = 0; y < this.MapHeight; ++y)
            {
                for (int x = 0; x < this.MapWidth; ++x)
                {
                    var location = new Point(x, y);
                    char height = input[y][x];

                    switch (height)
                    {
                        case 'S':
                            this.Start = location;
                            height = 'a';
                            break;

                        case 'E':
                            this.End = location;
                            height = 'z';
                            break;
                    }

                    this.Map[location] = height;
                }
            }
        }

        public Point Start { get; private set; }

        public Point End { get; private set; }

        public int MapHeight { get; private set; }

        public int MapWidth { get; private set; }

        public Dictionary<Point, char> Map { get; }

        private record struct State(Point Location, int Steps)
        {
        }

        public bool TryFindShortestPathBetween(Point start, Point end, [NotNullWhen(true)] out int? steps)
        {
            HashSet<Point> visitedLocations = new();
            Queue<State> states = new();
            states.Enqueue(new State(start, 0));

            Span<State> nextMoves = stackalloc State[4];

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

                int written = GetNextMoves(current, this.Map, this.MapHeight, this.MapWidth, nextMoves);
                for (int i = 0; i < written; ++i)
                {
                    states.Enqueue(nextMoves[i]);
                }
            }

            steps = null;
            return false;
        }

        private static bool CanMoveBetween(char here, char there)
        {
            return (there - here) < 2;
        }

        private static int GetNextMoves(State current, Dictionary<Point, char> map, int height, int width, Span<State> nextMoves)
        {
            int written = 0;
            Point next;
            char currentHeight = map[current.Location];

            if (current.Location.X > 0)
            {
                next = new Point(current.Location.X - 1, current.Location.Y);
                if (CanMoveBetween(currentHeight, map[next]))
                {
                    nextMoves[written++] = new State(next, current.Steps + 1);
                }
            }

            if (current.Location.X < (width - 1))
            {
                next = new Point(current.Location.X + 1, current.Location.Y);
                if (CanMoveBetween(currentHeight, map[next]))
                {
                    nextMoves[written++] = new State(next, current.Steps + 1);
                }
            }

            if (current.Location.Y > 0)
            {
                next = new Point(current.Location.X, current.Location.Y - 1);
                if (CanMoveBetween(currentHeight, map[next]))
                {
                    nextMoves[written++] = new State(next, current.Steps + 1);
                }
            }

            if (current.Location.Y < (height - 1))
            {
                next = new Point(current.Location.X, current.Location.Y + 1);
                if (CanMoveBetween(currentHeight, map[next]))
                {
                    nextMoves[written++] = new State(next, current.Steps + 1);
                }
            }

            return written;
        }
    }
}
