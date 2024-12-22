namespace AdventOfCode.Year2024.Day20;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    private static readonly Dictionary<(int X, int Y), int> ShortestDistancesHomeWithoutCheats = [];

    public string Solve(string[] input)
    {
        var map = new InvertedMap<char>(Map<char>.CreateCharMap(input));

        (int X, int Y)[] wallLocations = map['#'];

        ShortestDistancesHomeWithoutCheats.Add(map['E'][0], 0);

        int fastestRoute = GetShortestDistanceHomeWithoutCheats(map, map['S'][0]);

        Dictionary<(int X, int Y), int> routeTimesWithCheats = GetRouteTimesWithCheats(map, fastestRoute - 100);
        (int Key, int)[] groupedRouteTimesWithCheats = routeTimesWithCheats.Values.GroupBy(t => t).Select(g => (g.Key - fastestRoute, g.Count())).OrderBy(x => x.Item1).ToArray();

        return routeTimesWithCheats.Count.ToString();
    }

    private static int GetShortestDistanceHomeWithoutCheats(InvertedMap<char> map, (int X, int Y) start)
    {
        var visited = new HashSet<(int X, int Y)>();
        var queue = new Queue<PathStateWithoutCheats>();

        (int X, int Y) end = map['E'][0];

        queue.Enqueue(new(start, 0, []));
        while (queue.Count > 0)
        {
            PathStateWithoutCheats currentState = queue.Dequeue();

            if (ShortestDistancesHomeWithoutCheats.TryGetValue(currentState.Location, out int steps))
            {
                // We're in a location we've been to before. We can determine how far home by adding the steps we've already taken.
                // We also need to add our path to the cache.
                CachePath(currentState.Visited, steps + 1);
                return currentState.Steps + steps;
            }

            if (!map.IsLocationInBounds(currentState.Location) || visited.Contains(currentState.Location) || map.OriginalMap[currentState.Location] == '#')
            {
                continue;
            }

            visited.Add(currentState.Location);

            if (currentState.Location == end)
            {
                // Add the locations from the path into the cache. The list doesn't contain the final location, and is in reverse order.
                CachePath(currentState.Visited, 1);
                return currentState.Steps;
            }

            (int X, int Y)[] newPath = [currentState.Location, .. currentState.Visited];

            foreach (Direction2D direction in Direction2D.All)
            {
                queue.Enqueue(new(direction.GetNextLocation(currentState.Location), currentState.Steps + 1, newPath));
            }
        }

        return -1;
    }

    private static void CachePath((int X, int Y)[] pathInReverseOrder, int stepsHomeFromFirstLocation)
    {
        for (int i = 0; i < pathInReverseOrder.Length; ++i)
        {
            (int X, int Y) visitedLocation = pathInReverseOrder[i];

            Debug.Assert(!ShortestDistancesHomeWithoutCheats.ContainsKey(visitedLocation));
            ShortestDistancesHomeWithoutCheats.Add(visitedLocation, i + stepsHomeFromFirstLocation);
        }
    }

    private static Dictionary<(int X, int Y), int> GetRouteTimesWithCheats(InvertedMap<char> map, int maxSteps)
    {
        Dictionary<(int X, int Y), int> cheatLocationsWithTotalSteps = [];

        var queue = new Queue<PathStateWithCheats>();

        (int X, int Y) start = map['S'][0];
        (int X, int Y) end = map['E'][0];

        queue.Enqueue(new PathStateWithCheats(start, 0, [], null));

        while (queue.Count > 0)
        {
            PathStateWithCheats current = queue.Dequeue();

            if (!map.IsLocationInBounds(current.Location) || current.HasBeenHereBefore || current.Steps > maxSteps || (map.OriginalMap[current.Location] == '#' && current.CheatUsed))
            {
                continue;
            }

            if (current.CheatLocation.HasValue)
            {
                int distanceHomeFromHere = GetShortestDistanceHomeWithoutCheats(map, current.Location);
                int totalDistance = current.Steps + distanceHomeFromHere;

                if (totalDistance <= maxSteps)
                {
                    cheatLocationsWithTotalSteps.Add(current.CheatLocation.Value, current.Steps + distanceHomeFromHere);
                }

                continue;
            }

            (int X, int Y)? newCheatLocation = null;

            // If we're in a wall, then we've used our cheat and it's no longer available
            if (!current.CheatUsed && map.OriginalMap[current.Location] == '#')
            {
                newCheatLocation = current.Location;
            }

            List<(int X, int Y)> newVisited = [current.Location, .. current.Visited];

            foreach (Direction2D direction in Direction2D.All)
            {
                queue.Enqueue(new PathStateWithCheats(direction.GetNextLocation(current.Location), current.Steps + 1, newVisited, newCheatLocation));
            }
        }

        return cheatLocationsWithTotalSteps;
    }

    private readonly record struct PathStateWithoutCheats((int X, int Y) Location, int Steps, (int X, int Y)[] Visited);

    private readonly record struct PathStateWithCheats((int X, int Y) Location, int Steps, List<(int X, int Y)> Visited, (int X, int Y)? CheatLocation)
    {
        public bool HasBeenHereBefore => this.Visited.Contains(this.Location);

        public bool CheatUsed => this.CheatLocation.HasValue;
    }
}