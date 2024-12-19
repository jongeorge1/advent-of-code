namespace AdventOfCode.Year2024.Day18;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        bool isTest = input[0] == "TEST";

        int maxX = isTest ? 6 : 70;
        int maxY = isTest ? 6 : 70;

        input = isTest ? input[1..] : input;

        (int X, int Y)[] corruptedLocations = [..input.Select(line =>
        {
            string[] parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            return (x, y);
        })];

        // We can safely start at byte 1024 as we found a path to the exit... to avoid being lazy, testing all the locations in
        // sequence and taking forever, we will do a binary search. We'll need to test values in pairs. I did have a cache here,
        // but in reality we're only likely to evaluate the same item more than once when we reach the end of the search.
        // Also, there should be something to kick you out of the loop if no match is found, but we know there's going to be one
        // so I didn't bother.
        int minByte = isTest ? 12 : 1024;
        int maxByte = corruptedLocations.Length - 1;

        while (minByte <= maxByte)
        {
            int targetByte = minByte + ((maxByte - minByte) / 2);

            bool previousHasAPath = CanReachExit(maxX, maxY, corruptedLocations[0..targetByte]);
            bool currentHasAPath = CanReachExit(maxX, maxY, corruptedLocations[0..(targetByte + 1)]);

            if (previousHasAPath && !currentHasAPath)
            {
                return input[targetByte];
            }

            if (currentHasAPath)
            {
                minByte = targetByte;
            }
            else if (!previousHasAPath)
            {
                maxByte = targetByte - 1;
            }
        }

        throw new Exception("No solution found");
    }

    private static bool CanReachExit(int maxX, int maxY, (int X, int Y)[] corruptedLocations)
    {
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
                return true;
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

        return false;
    }

    private readonly record struct PathState((int X, int Y) Location, int Steps, HashSet<(int X, int Y)> VisitedLocations);
}
