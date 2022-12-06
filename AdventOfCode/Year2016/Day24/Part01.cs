using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2016.Day24
{
    public class Part01 : ISolution
    {
        private const char Wall = '#';
        private const char Poi = 'P';
        private const char Space = '.';

        public string Solve(string input)
        {
            // First we need to parse the input and build a map of points.
            var map = new Dictionary<(int X, int Y), char>();
            (int X, int Y) startLocation = (0, 0);
            int poiCount = 0;
            var allPois = new List<(int X, int Y)>();

            string[] rows = input.Split(Environment.NewLine);

            for (int y = 0; y < rows.Length; ++y)
            {
                for (int x = 0; x < rows[0].Length; ++x)
                {
                    switch (rows[y][x])
                    {
                        case '#':
                            map[(x, y)] = Wall;
                            break;

                        case '0':
                            startLocation = (x, y);
                            map[(x, y)] = Space;
                            break;

                        case '.':
                            map[(x, y)] = Space;
                            break;

                        default:
                            map[(x, y)] = Poi;
                            allPois.Add((x, y));
                            ++poiCount;
                            break;
                    }
                }
            }

            // Now we will do a basic breadth first search to find the route that hits all the POIs first.
            // This is a bit of an odd one since we are allowed to visit spaces more than once, so we can't
            // do the normal thing of dropping routes if we hit a space for the second time. This means we'll
            // need to find an alternate way of prioritising the queue...
            // For each route on the queue, we need to track:
            // 1. Which POIs it has collected already
            // 2. Where it was in the previous step. We will allow "turning around", but only when we hit
            //    a POI that we didn't previously have.
            var routeQueue = new PriorityQueue<RouteState, int>();

            var initialState = new RouteState
            {
                Position = startLocation,
                PreviousPosition = null,
                CollectedPois = new List<(int X, int Y)>(),
                Steps = 0,
            };

            routeQueue.Enqueue(initialState, 0);

            while (routeQueue.Count > 0)
            {
                RouteState current = routeQueue.Dequeue();
                bool justCollectedAPoi = false;

                var nextPoisList = new List<(int, int)>(current.CollectedPois);

                if (allPois.Contains(current.Position) && !current.CollectedPois.Contains(current.Position))
                {
                    // We're on a POI we haven't seen before.
                    nextPoisList.Add(current.Position);
                    justCollectedAPoi = true;

                    // If we've got all the POIs we're done
                    if (nextPoisList.Count == poiCount)
                    {
                        return current.Steps.ToString();
                    }
                }

                // Where to next?
                foreach ((int X, int Y) nextPosition in PossibleNextPositions(current.Position, current.PreviousPosition, map, justCollectedAPoi))
                {
                    routeQueue.Enqueue(
                        new RouteState
                        {
                            Position = nextPosition,
                            PreviousPosition = current.Position,
                            CollectedPois = new List<(int, int)>(nextPoisList),
                            Steps = current.Steps + 1,
                        },
                        ((poiCount - nextPoisList.Count) * 10000) + current.Steps + 1);

                    if (routeQueue.Count % 10000 == 0)
                    {
                        Console.WriteLine($"RouteQueue contains {routeQueue.Count} entries");
                    }
                }
            }

            return "It went wrong";
        }

        private static int ShortestDistanceBetweenTwoPoints()
        {
            return 0;
        }

        private static IEnumerable<(int X, int Y)> PossibleNextPositions((int X, int Y) currentPosition, (int X, int Y)? previousPosition, Dictionary<(int X, int Y), char> map, bool justCollectedAPoi)
        {
            (int, int)[] possibles = new[]
            {
                (currentPosition.X, currentPosition.Y + 1),
                (currentPosition.X, currentPosition.Y - 1),
                (currentPosition.X + 1, currentPosition.Y),
                (currentPosition.X - 1, currentPosition.Y),
            };

            foreach (var possible in possibles)
            {
                // We can move to the space if:
                // 1. It's not a wall, and
                // 2. It's not where we were previously UNLESS we're currently on a POI, in which case we allow backtracking.
                if (map[possible] != Wall && (possible != previousPosition || justCollectedAPoi))
                {
                    yield return possible;
                }
            }
        }

        private class RouteState
        {
            public (int X, int Y) Position { get; set; }

            public (int X, int Y)? PreviousPosition { get; set; }

            public List<(int X, int Y)> CollectedPois { get; set; }

            public int Steps { get; set; }
        }
    }
}