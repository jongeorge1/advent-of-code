namespace AdventOfCode.Year2023.Day17;

using System;
using System.Collections.Generic;
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

        var lowestSeenHeatLosses = heatLossMap.Keys.SelectMany<(int X, int Y), ((int X, int Y) Location, Direction2D Direction)>(x => [(x, Direction2D.North), (x, Direction2D.East), (x, Direction2D.South), (x, Direction2D.West)]).ToDictionary(x => x, x => int.MaxValue);

        var queue = new PriorityQueue<PathState, int>();

        queue.Enqueue(new PathState((0, 0), [], 0, 0, Direction2D.East, 0), 0);

        while (queue.Count > 0)
        {
            PathState current = queue.Dequeue();

            if (current.Location == destination)
            {
                for (int row = 0; row <= maxRow; ++row)
                {
                    for (int col = 0; col <= maxCol; ++col)
                    {
                        if (current.VisitedLocations.Contains((col, row)))
                        {
                            Console.Write("O");
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
            if (lowestSeenHeatLosses[(current.Location, current.Direction)] <= current.HeatLoss)
            {
                continue;
            }

            lowestSeenHeatLosses[(current.Location, current.Direction)] = current.HeatLoss;

            // Add next steps based on going straight, turning left and turning right.
            if (current.StepsSinceLastTurn < 3)
            {
                (int X, int Y) straightLocation = current.Direction.GetNextLocation(current.Location);
                if (IsLocationValid(straightLocation, maxRow, maxCol) && !current.VisitedLocations.Contains(straightLocation))
                {
                    var nextState = new PathState(straightLocation, [.. current.VisitedLocations, current.Location], current.Steps + 1, current.HeatLoss + heatLossMap[straightLocation], current.Direction, current.StepsSinceLastTurn + 1);
                    queue.Enqueue(nextState, nextState.HeatLoss);
                }
            }

            (int X, int Y) leftLocation = current.Direction.Left.GetNextLocation(current.Location);
            if (IsLocationValid(leftLocation, maxRow, maxCol) && !current.VisitedLocations.Contains(leftLocation))
            {
                var nextState = new PathState(leftLocation, [.. current.VisitedLocations, current.Location], current.Steps + 1, current.HeatLoss + heatLossMap[leftLocation], current.Direction.Left, 1);
                queue.Enqueue(nextState, nextState.HeatLoss);
            }

            (int X, int Y) rightLocation = current.Direction.Right.GetNextLocation(current.Location);
            if (IsLocationValid(rightLocation, maxRow, maxCol) && !current.VisitedLocations.Contains(rightLocation))
            {
                var nextState = new PathState(rightLocation, [.. current.VisitedLocations, current.Location], current.Steps + 1, current.HeatLoss + heatLossMap[rightLocation], current.Direction.Right, 1);
                queue.Enqueue(nextState, nextState.HeatLoss);
            }
        }

        throw new Exception("No path found");
    }

    private static bool IsLocationValid((int X, int Y) location, int maxRow, int maxCol) => location.X >= 0 && location.X <= maxCol && location.Y >= 0 && location.Y <= maxRow;

    public readonly record struct PathState((int X, int Y) Location, List<(int X, int Y)> VisitedLocations, int Steps, int HeatLoss, Direction2D Direction, int StepsSinceLastTurn);
}
