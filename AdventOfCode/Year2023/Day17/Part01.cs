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
        Dictionary<(int X, int Y), int> heatLossMap = new();

        for (int row = 0; row < input.Length; ++row)
        {
            for (int col = 0; col < input.Length; ++col)
            {
                heatLossMap[(col, row)] = input[row][col] - 48;
            }
        }

        int maxRow = input.Length - 1;
        int maxCol = input[0].Length - 1;
        (int X, int Y) destination = (maxCol, maxRow);

        var lowestSeenHeatLosses = heatLossMap.Keys.SelectMany<(int X, int Y), ((int X, int Y) Location, Direction2D.Orientations Orientation)>(x => [(x, Direction2D.Orientations.Vertical), (x, Direction2D.Orientations.Horizontal)]).ToDictionary(x => x, x => int.MaxValue);

        var queue = new PriorityQueue<PathState, int>();

        queue.Enqueue(new PathState((0, 0), [], 0, 0, Direction2D.East, 0), 0);

        while (queue.Count > 0)
        {
            PathState current = queue.Dequeue();

            if (current.Location == destination)
            {
                ((int X, int Y) Location, Direction2D Direction)[] allVisitedLocations = [..current.VisitedLocations, (current.Location, current.Direction)];

                for (int row = 0; row <= maxRow; ++row)
                {
                    for (int col = 0; col <= maxCol; ++col)
                    {
                        if (allVisitedLocations.Contains(((col, row), Direction2D.North)))
                        {
                            Console.Write("^");
                        }
                        else if (allVisitedLocations.Contains(((col, row), Direction2D.East)))
                        {
                            Console.Write(">");
                        }
                        else if (allVisitedLocations.Contains(((col, row), Direction2D.West)))
                        {
                            Console.Write("<");
                        }
                        else if (allVisitedLocations.Contains(((col, row), Direction2D.South)))
                        {
                            Console.Write("v");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }

                    Console.WriteLine();
                }

                return current.HeatLoss.ToString();
            }

            // Have we been here before in the same direction with a lower heat loss? If so, abandon this path.
            if (lowestSeenHeatLosses[(current.Location, current.Direction.Orientation)] <= current.HeatLoss)
            {
                continue;
            }

            lowestSeenHeatLosses[(current.Location, current.Direction.Orientation)] = current.HeatLoss;

            (int X, int Y) straightLocation = current.Direction.GetNextLocation(current.Location);
            (int X, int Y) leftLocation = current.Direction.Left.GetNextLocation(current.Location);
            (int X, int Y) rightLocation = current.Direction.Right.GetNextLocation(current.Location);

            List<((int X, int Y) Location, Direction2D Direction)> newVisitedLocations = [.. current.VisitedLocations, (current.Location, current.Direction)];

            PathState[] possibleNextPathStates =
                [
                    new PathState(straightLocation, newVisitedLocations, current.Steps + 1, current.HeatLoss + heatLossMap[straightLocation], current.Direction, current.StepsSinceLastTurn + 1),
                    new PathState(leftLocation, newVisitedLocations, current.Steps + 1, current.HeatLoss + heatLossMap[leftLocation], current.Direction.Left, 1),
                    new PathState(rightLocation, newVisitedLocations, current.Steps + 1, current.HeatLoss + heatLossMap[rightLocation], current.Direction.Right, 1),
                ];

            foreach (PathState state in possibleNextPathStates)
            {
                if (IsStateValid(state, maxRow, maxCol, current.VisitedLocations))
                {
                    queue.Enqueue(state, state.HeatLoss);
                }
            }
        }

        throw new Exception("No path found");
    }

    private static bool IsStateValid(PathState state, int maxRow, int maxCol, List<((int X, int Y) Location, Direction2D Direction)> visitedLocations)
    {
        return state.StepsSinceLastTurn <= 3 && IsLocationValid(state.Location, maxRow, maxCol) && !visitedLocations.Contains((state.Location, state.Direction));
    }

    private static bool IsLocationValid((int X, int Y) location, int maxRow, int maxCol) => location.X >= 0 && location.X <= maxCol && location.Y >= 0 && location.Y <= maxRow;

    [DebuggerDisplay("At ({Location.X}, {Location.Y}) heading {Direction.Name}, Heat loss {HeatLoss}, Steps since last turn {StepsSinceLastTurn}")]
    public readonly record struct PathState((int X, int Y) Location, List<((int X, int Y) Location, Direction2D Direction)> VisitedLocations, int Steps, int HeatLoss, Direction2D Direction, int StepsSinceLastTurn);
}
