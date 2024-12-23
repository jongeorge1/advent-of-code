namespace AdventOfCode.Year2024.Day20;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        const int SpeedImprovementRequirement = 100;

        var map = new InvertedMap<char>(Map<char>.CreateCharMap(input));
        (int X, int Y)[] wallLocations = map['#'];
        (int X, int Y)[] route = GetRoute(map, map['S'][0]);

        List<int> cheats = [];

        // Now we step through the route adding cheats that save at least SpeedImprovementRequirement steps.
        for (int startLocationIndex = 0; startLocationIndex < route.Length - SpeedImprovementRequirement; ++startLocationIndex)
        {
            // Find all the locations we can cheat to from here. This is going to be all the locations in the path
            // from i = SpeedImprovementRequirement onwards that are within 20 steps of our current location
            (int X, int Y) currentLocation = route[startLocationIndex];

            for (int endLocationIndex = startLocationIndex + SpeedImprovementRequirement; endLocationIndex < route.Length; ++endLocationIndex)
            {
                int cheatDistance = Distance.Manhattan(route[startLocationIndex], route[endLocationIndex]);
                if (cheatDistance <= 2)
                {
                    // We've got a possible valid cheat.
                    int stepsSaved = endLocationIndex - startLocationIndex - cheatDistance;
                    if (stepsSaved >= SpeedImprovementRequirement)
                    {
                        cheats.Add(stepsSaved);
                    }
                }
            }
        }

        return cheats.Count.ToString();
    }

    private static (int X, int Y)[] GetRoute(InvertedMap<char> map, (int X, int Y) start)
    {
        var queue = new Queue<PathState>();

        (int X, int Y) end = map['E'][0];

        queue.Enqueue(new(start, 0, []));

        while (queue.Count > 0)
        {
            PathState currentState = queue.Dequeue();

            if (!map.IsLocationInBounds(currentState.Location) || currentState.Visited.Contains(currentState.Location) || map.OriginalMap[currentState.Location] == '#')
            {
                continue;
            }

            (int X, int Y)[] newPath = [.. currentState.Visited, currentState.Location];

            if (currentState.Location == end)
            {
                return newPath;
            }

            foreach (Direction2D direction in Direction2D.All)
            {
                queue.Enqueue(new(direction.GetNextLocation(currentState.Location), currentState.Steps + 1, newPath));
            }
        }

        throw new Exception("Route not found");
    }

    private readonly record struct PathState((int X, int Y) Location, int Steps, (int X, int Y)[] Visited);
}
