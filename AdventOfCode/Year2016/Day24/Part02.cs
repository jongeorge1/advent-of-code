namespace AdventOfCode.Year2016.Day24
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // First we need to parse the input and build a map of points.
            var map = new HashSet<(int X, int Y)>();
            (int X, int Y) startLocation = (0, 0);
            int poiCount = 0;
            var allPois = new List<(int X, int Y)>();

            int y = 0;
            foreach (string row in input)
            {
                for (int x = 0; x < row.Length; ++x)
                {
                    switch (row[x])
                    {
                        case '#':
                            break;

                        case '0':
                            startLocation = (x, y);
                            map.Add((x, y));
                            break;

                        case '.':
                            map.Add((x, y));
                            break;

                        default:
                            map.Add((x, y));
                            allPois.Add((x, y));
                            ++poiCount;
                            break;
                    }
                }

                ++y;
            }

            Dictionary<((int X, int Y) From, (int X, int Y) To), int> shortestPaths = new();

            // Next step is a BFS to find the distances between all the POIs in the map. We also need the distances from the start position to
            // each POI.
            foreach ((int X, int Y) from in allPois)
            {
                shortestPaths.Add((startLocation, from), FindShortestPath(map, startLocation, from));

                foreach ((int X, int Y) to in allPois)
                {
                    if (from == to)
                    {
                        continue;
                    }

                    if (shortestPaths.TryGetValue((to, from), out int result))
                    {
                        shortestPaths.Add((from, to), result);
                    }
                    else
                    {
                        shortestPaths.Add((from, to), FindShortestPath(map, from, to));
                    }
                }
            }

            // Now we need to look at the best sequence to visit the POIs in. We always need to start at the beginning
            Queue<RouteState> stateQueue = new();
            int shortestPathSoFar = int.MaxValue;

            var state = new RouteState
            {
                CollectedPois = new List<(int X, int Y)>(),
                Location = startLocation,
                Steps = 0,
            };

            stateQueue.Enqueue(state);

            while (stateQueue.Count > 0)
            {
                RouteState current = stateQueue.Dequeue();

                if (current.Steps >= shortestPathSoFar)
                {
                    // Abandon this route
                    continue;
                }

                if (current.CollectedPois.Count == allPois.Count)
                {
                    // We've got all the POIs. But are we back at the target?
                    if (current.Location == startLocation)
                    {
                        shortestPathSoFar = current.Steps;
                    }
                    else
                    {
                        var next = new RouteState
                        {
                            CollectedPois = new List<(int X, int Y)>(current.CollectedPois),
                            Location = startLocation,
                            Steps = current.Steps + shortestPaths[(startLocation, current.Location)],
                        };

                        stateQueue.Enqueue(next);
                    }

                    continue;
                }

                // Continue on this path
                foreach ((int X, int Y) poi in allPois)
                {
                    if (!current.CollectedPois.Contains(poi))
                    {
                        var next = new RouteState
                        {
                            CollectedPois = new List<(int X, int Y)>(current.CollectedPois),
                            Location = poi,
                            Steps = current.Steps + shortestPaths[(current.Location, poi)],
                        };

                        next.CollectedPois.Add(poi);

                        stateQueue.Enqueue(next);
                    }
                }
            }

            return shortestPathSoFar.ToString();
        }

        private static int FindShortestPath(HashSet<(int X, int Y)> map, (int X, int Y) from, (int X, int Y) to)
        {
            // Standard BFS for the shortest path.
            Queue<((int X, int Y) Location, int Steps)> pathQueue = new();
            HashSet<(int X, int Y)> visitedLocations = new();

            pathQueue.Enqueue((from, 0));

            while (pathQueue.Count > 0)
            {
                ((int X, int Y) Location, int Steps) current = pathQueue.Dequeue();

                if (current.Location == to)
                {
                    return current.Steps;
                }

                if (visitedLocations.Contains(current.Location))
                {
                    continue;
                }

                visitedLocations.Add(current.Location);

                foreach ((int X, int Y) next in PossibleNextPositions(current.Location, map))
                {
                    pathQueue.Enqueue((next, current.Steps + 1));
                }
            }

            throw new Exception();
        }

        private static IEnumerable<(int X, int Y)> PossibleNextPositions((int X, int Y) currentPosition, HashSet<(int X, int Y)> map)
        {
            (int X, int) next = (currentPosition.X, currentPosition.Y + 1);
            if (map.Contains(next))
            {
                yield return next;
            }

            next = (currentPosition.X, currentPosition.Y - 1);
            if (map.Contains(next))
            {
                yield return next;
            }

            next = (currentPosition.X + 1, currentPosition.Y);
            if (map.Contains(next))
            {
                yield return next;
            }

            next = (currentPosition.X - 1, currentPosition.Y);
            if (map.Contains(next))
            {
                yield return next;
            }
        }

        private class RouteState
        {
            public List<(int X, int Y)> CollectedPois { get; set; }

            public (int X, int Y) Location { get; set; }

            public int Steps { get; set; }
        }
    }
}