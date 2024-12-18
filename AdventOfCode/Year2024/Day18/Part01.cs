namespace AdventOfCode.Year2024.Day18;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        bool isTest = input[0] == "TEST";

        int maxX = isTest ? 6 : 70;
        int maxY = isTest ? 6 : 70;

        input = isTest ? input[1..13] : input[..1024];

        HashSet<(int X, int Y)> corruptedLocations = input.Select(line =>
        {
            string[] parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            return (x, y);
        }).ToHashSet();

        // Basic BFS to find the shortest path
        (int X, int Y) targetLocation = (maxX, maxY);
        Queue<PathState> queue = new();
        Dictionary<(int X, int Y), int> locationMinimumSteps = [];

        queue.Enqueue(new PathState((0, 0), 0, [(0, 0)]));

        while (queue.Count > 0)
        {
            PathState currentState = queue.Dequeue();

            if (currentState.Location == targetLocation)
            {
                return currentState.Steps.ToString();
            }

            if (locationMinimumSteps.TryGetValue(currentState.Location, out int minimumSteps) && minimumSteps <= currentState.Steps)
            {
                continue;
            }

            locationMinimumSteps[currentState.Location] = currentState.Steps;

            foreach (Direction2D direction in Direction2D.All)
            {
                (int X, int Y) nextLocation = direction.GetNextLocation(currentState.Location);

                if (nextLocation.X < 0 || nextLocation.Y < 0 || nextLocation.X > maxX || nextLocation.Y > maxY)
                {
                    continue;
                }

                if (corruptedLocations.Contains(nextLocation))
                {
                    continue;
                }

                if (currentState.VisitedLocations.Contains(nextLocation))
                {
                    continue;
                }

                queue.Enqueue(new PathState(nextLocation, currentState.Steps + 1, [currentState.Location, .. currentState.VisitedLocations]));
            }
        }

        throw new Exception("No path found");
    }

    private readonly record struct PathState((int X, int Y) Location, int Steps, HashSet<(int X, int Y)> VisitedLocations);
}
