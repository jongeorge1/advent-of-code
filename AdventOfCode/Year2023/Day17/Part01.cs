namespace AdventOfCode.Year2023.Day17;

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
        var map = Map<int>.CreateDigitMap(input);

        (int X, int Y) destination = (map.MaxX, map.MaxY);

        Dictionary<((int X, int Y) Location, Direction2D Direction, int StepsSinceLastTurn), int> lowestSeenHeatLosses = [];

        var queue = new PriorityQueue<PathState, int>();

        queue.Enqueue(new PathState((0, 0), [], 0, 0, Direction2D.East, 0), 0);

        while (queue.Count > 0)
        {
            PathState current = queue.Dequeue();

            if (current.Location == destination)
            {
                ////((int X, int Y) Location, Direction2D Direction)[] allVisitedLocations = [.. current.VisitedLocations, (current.Location, current.Direction)];

                ////for (int row = 0; row <= map.MaxY; ++row)
                ////{
                ////    for (int col = 0; col <= map.MaxX; ++col)
                ////    {
                ////        if (allVisitedLocations.Contains(((col, row), Direction2D.North)))
                ////        {
                ////            Console.Write("^");
                ////        }
                ////        else if (allVisitedLocations.Contains(((col, row), Direction2D.East)))
                ////        {
                ////            Console.Write(">");
                ////        }
                ////        else if (allVisitedLocations.Contains(((col, row), Direction2D.West)))
                ////        {
                ////            Console.Write("<");
                ////        }
                ////        else if (allVisitedLocations.Contains(((col, row), Direction2D.South)))
                ////        {
                ////            Console.Write("v");
                ////        }
                ////        else
                ////        {
                ////            Console.Write(".");
                ////        }
                ////    }

                ////    Console.WriteLine();
                ////}

                return current.HeatLoss.ToString();
            }

            // Have we been here before in the same direction with a lower heat loss? If so, abandon this path.
            if (lowestSeenHeatLosses.TryGetValue((current.Location, current.Direction, current.StepsSinceLastTurn), out int lowestHeatLoss) && lowestHeatLoss <= current.HeatLoss)
            {
                continue;
            }

            lowestSeenHeatLosses[(current.Location, current.Direction, current.StepsSinceLastTurn)] = current.HeatLoss;

            List<((int X, int Y) Location, Direction2D Direction)> newVisitedLocations = [.. current.VisitedLocations, (current.Location, current.Direction)];

            if (current.StepsSinceLastTurn < 3)
            {
                (int X, int Y) straightLocation = current.Direction.GetNextLocation(current.Location);
                if (map.IsLocationInBounds(straightLocation) && !current.VisitedLocations.Contains((straightLocation, current.Direction)))
                {
                    int heatLoss = current.HeatLoss + map[straightLocation];
                    queue.Enqueue(new PathState(straightLocation, newVisitedLocations, current.Steps + 1, heatLoss, current.Direction, current.StepsSinceLastTurn + 1), heatLoss);
                }
            }

            (int X, int Y) leftLocation = current.Direction.Left.GetNextLocation(current.Location);
            if (map.IsLocationInBounds(leftLocation) && !current.VisitedLocations.Contains((leftLocation, current.Direction.Left)))
            {
                int heatLoss = current.HeatLoss + map[leftLocation];
                queue.Enqueue(new PathState(leftLocation, newVisitedLocations, current.Steps + 1, heatLoss, current.Direction.Left, 1), heatLoss);
            }

            (int X, int Y) rightLocation = current.Direction.Right.GetNextLocation(current.Location);
            if (map.IsLocationInBounds(rightLocation) && !current.VisitedLocations.Contains((rightLocation, current.Direction.Right)))
            {
                int heatLoss = current.HeatLoss + map[rightLocation];
                queue.Enqueue(new PathState(rightLocation, newVisitedLocations, current.Steps + 1, heatLoss, current.Direction.Right, 1), heatLoss);
            }
        }

        throw new Exception("No path found");
    }

    [DebuggerDisplay("At ({Location.X}, {Location.Y}) heading {Direction.Name}, Heat loss {HeatLoss}, Steps since last turn {StepsSinceLastTurn}")]
    public readonly record struct PathState((int X, int Y) Location, List<((int X, int Y) Location, Direction2D Direction)> VisitedLocations, int Steps, int HeatLoss, Direction2D Direction, int StepsSinceLastTurn);
}
