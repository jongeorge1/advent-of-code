namespace AdventOfCode.Year2023.Day17;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        var map = Map<Node>.Create(input, Node.TryCreateFromInputItem);
        foreach (Node node in map.Values)
        {
            node.AddEdges(map);
        }

        Node start = map[(0, 0)];
        Node destination = map[(map.MaxX, map.MaxY)];

        PriorityQueue<(Node, Direction2D.Orientations), int> unvisited = new();
        unvisited.Enqueue((start, Direction2D.Orientations.Horizontal), 0);
        unvisited.Enqueue((start, Direction2D.Orientations.Vertical), 0);

        var heatLosses = map.Values
            .SelectMany<Node, (Node, Direction2D.Orientations)>(node => [(node, Direction2D.Orientations.Vertical), (node, Direction2D.Orientations.Horizontal)])
            .ToDictionary(x => x, _ => int.MaxValue);

        heatLosses[(start, Direction2D.Orientations.Vertical)] = 0;
        heatLosses[(start, Direction2D.Orientations.Horizontal)] = 0;

        while (unvisited.Count > 0)
        {
            (Node currentNode, Direction2D.Orientations currentOrientation) = unvisited.Dequeue();

            if (currentNode == destination)
            {
                return heatLosses[(currentNode, currentOrientation)].ToString();
            }

            int currentNodeHeatLoss = heatLosses[(currentNode, currentOrientation)];

            // Edges to consider are the ones that are in the opporsite orientation to the one we are currently in.
            Direction2D.Orientations targetOrientation = Direction2D.GetPerpendicularOrientation(currentOrientation);
            IEnumerable<Edge> edges = currentNode.Edges.Where(edge => edge.Direction.Orientation == targetOrientation);

            foreach (Edge edge in edges)
            {
                int targetNodeCurrentHeatLoss = heatLosses[(edge.End, targetOrientation)];
                int targetNodeHeatlossViaThisEdge = currentNodeHeatLoss + edge.HeatLoss;

                if (targetNodeHeatlossViaThisEdge < targetNodeCurrentHeatLoss)
                {
                    heatLosses[(edge.End, targetOrientation)] = targetNodeHeatlossViaThisEdge;
                    unvisited.Remove((edge.End, targetOrientation), out _, out _);
                    unvisited.Enqueue((edge.End, targetOrientation), targetNodeHeatlossViaThisEdge);
                }
            }
        }

        throw new Exception("No path found");
    }

    [DebuggerDisplay("({Location.X}, {Location.Y}): {HeatLossOnEntry}")]
    public class Node((int X, int Y) location, int heatLossOnEntry)
    {
        private Edge[]? edges;

        public (int X, int Y) Location { get; } = location;

        public int HeatLossOnEntry { get; } = heatLossOnEntry;

        public Edge[] Edges
        {
            get => this.edges ?? throw new InvalidOperationException("Edges has not been initialised");
            set => this.edges = value;
        }

        public static bool TryCreateFromInputItem(char characterAtLocation, (int X, int Y) location, [MaybeNullWhen(false)] out Node? item)
        {
            item = new Node(location, characterAtLocation - '0');
            return true;
        }

        public void AddEdges(Map<Node> map)
        {
            this.Edges = Direction2D.All.SelectMany(direction => this.CreateEdges(direction, 4, 10, map)).ToArray();
        }

        private Edge[] CreateEdges(Direction2D direction, int minSteps, int maxSteps, Map<Node> map)
        {
            // Move the minimum number of steps, aggregating heat losses as we go.
            int steps = 1;
            int heatLossSoFar = 0;
            (int X, int Y) currentLocation = this.Location;

            while (steps < minSteps)
            {
                currentLocation = direction.GetNextLocation(currentLocation);

                if (!map.IsLocationInBounds(currentLocation))
                {
                    return [];
                }

                heatLossSoFar += map[currentLocation].HeatLossOnEntry;

                ++steps;
            }

            List<Edge> edges = [];

            while (steps <= maxSteps)
            {
                currentLocation = direction.GetNextLocation(currentLocation);

                if (!map.IsLocationInBounds(currentLocation))
                {
                    break;
                }

                heatLossSoFar += map[currentLocation].HeatLossOnEntry;

                edges.Add(new Edge(direction, map[currentLocation], steps, heatLossSoFar));

                ++steps;
            }

            return [.. edges];
        }
    }

    [DebuggerDisplay("{Steps} steps {Direction.Name} to ({End.Location.X}, {End.Location.Y}) with heat loss {HeatLoss}")]
    public record Edge(Direction2D Direction, Node End, int Steps, int HeatLoss);
}
