namespace AdventOfCode.Year2024.Day16;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        var map = Map<char>.CreateCharMap(input);
        var invertedMap = new InvertedMap<char>(map);

        (int X, int Y)[] wallLocations = invertedMap['#'];
        (int X, int Y) startLocation = invertedMap['S'][0];
        (int X, int Y) endLocation = invertedMap['E'][0];

        PriorityQueue<PathState, int> queue = new();
        Dictionary<((int X, int Y) Location, Direction2D Direction), int> bestSeenScores = new();

        queue.Enqueue(new PathState(startLocation, Direction2D.East, 0, []), 0);

        while (queue.Count > 0)
        {
            PathState current = queue.Dequeue();

            if (current.Location == endLocation)
            {
                return current.Score.ToString();
            }

            if (bestSeenScores.TryGetValue((current.Location, current.Direction), out int bestScore) && bestScore < current.Score)
            {
                continue;
            }

            bestSeenScores[(current.Location, current.Direction)] = current.Score;

            // Add new state for straight on.
            (int X, int Y) straightOnLocation = current.Direction.GetNextLocation(current.Location);
            if (!current.VisitedLocations.Contains(straightOnLocation) && !wallLocations.Contains(straightOnLocation))
            {
                queue.Enqueue(new PathState(straightOnLocation, current.Direction, current.Score + 1, [current.Location, .. current.VisitedLocations]), current.Score + 1);
            }

            // Add new states for turning. There will never be a case for a backtrack, so we can do turn and move forward in one go.
            (int X, int Y) leftLocation = current.Direction.Left.GetNextLocation(current.Location);
            if (!current.VisitedLocations.Contains(leftLocation) && !wallLocations.Contains(leftLocation))
            {
                int newScore = current.Score + 1001;
                queue.Enqueue(new PathState(leftLocation, current.Direction.Left, newScore, [current.Location, .. current.VisitedLocations]), newScore);
            }

            (int X, int Y) rightLocation = current.Direction.Right.GetNextLocation(current.Location);
            if (!current.VisitedLocations.Contains(rightLocation) && !wallLocations.Contains(rightLocation))
            {
                int newScore = current.Score + 1001;
                queue.Enqueue(new PathState(rightLocation, current.Direction.Right, newScore, [current.Location, .. current.VisitedLocations]), newScore);
            }
        }

        throw new Exception("No path found");
    }

    public readonly record struct PathState((int X, int Y) Location, Direction2D Direction, int Score, (int X, int Y)[] VisitedLocations);
}
