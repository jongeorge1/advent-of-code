namespace AdventOfCode.Year2025.Day08;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int connectionsToMake = input.Length > 100 ? 1000 : 10;

        List<(int X, int Y, int Z)> junctionBoxes = [..input.Select(r =>
        {
            string[] components = r.Split(",");
            return (int.Parse(components[0]), int.Parse(components[1]), int.Parse(components[2]));
        })];

        List<List<(int X, int Y, int Z)>> circuits = [];
        List<((int X, int Y, int Z) Left, (int X, int Y, int Z) Right, double Distance)> distances = BuildDistances(junctionBoxes);

        // We need to process the boxes in order of distance from one another
        distances = [.. distances.OrderBy(x => x.Distance)];

        for (int i = 0; i < connectionsToMake; ++i)
        {
            ((int X, int Y, int Z) left, (int X, int Y, int Z) right, _) = distances[i];
            WireUp(circuits, left, right);
        }

        // Calculate result
        int result = circuits.Select(x => x.Count).OrderByDescending(x => x).Take(3).Product();
        return result.ToString();
    }

    private static List<((int X, int Y, int Z) Left, (int X, int Y, int Z) Right, double Distance)> BuildDistances(List<(int X, int Y, int Z)> junctionBoxes)
    {
        List<((int X, int Y, int Z) Left, (int X, int Y, int Z) Right)> processedPairs = [];
        List<((int X, int Y, int Z) Left, (int X, int Y, int Z) Right, double Distance)> result = [];

        for (int leftIndex = 0; leftIndex < junctionBoxes.Count; leftIndex++)
        {
            for (int rightIndex = 0; rightIndex < junctionBoxes.Count; rightIndex++)
            {
                if (leftIndex == rightIndex)
                {
                    // No need to compare a box to itself.
                    continue;
                }

                if (rightIndex < leftIndex)
                {
                    // We'll already have this combination
                    continue;
                }

                (int X, int Y, int Z) left = junctionBoxes[leftIndex];
                (int X, int Y, int Z) right = junctionBoxes[rightIndex];

                double distance = Distance.StraightLine(left, right);

                Debug.Assert(!double.IsNaN(distance));

                processedPairs.Add((left, right));
                result.Add((left, right, distance));
            }
        }

        return result;
    }

    private static List<(int X, int Y, int Z)>? FindExistingCircuitFor((int X, int Y, int Z) junctionBox, List<List<(int X, int Y, int Z)>> circuits)
    {
        return circuits.SingleOrDefault(c => c.Contains(junctionBox));
    }

    private static void WireUp(List<List<(int X, int Y, int Z)>> circuits, (int X, int Y, int Z) left, (int X, int Y, int Z) right)
    {
        List<(int X, int Y, int Z)>? existingLeftCircuit = FindExistingCircuitFor(left, circuits);
        List<(int X, int Y, int Z)>? existingRightCircuit = FindExistingCircuitFor(right, circuits);

        // No existing circuits for either: new circuit.
        if (existingLeftCircuit is null && existingRightCircuit is null)
        {
            circuits.Add([left, right]);
            return;
        }

        // Both already in the same circuit: do nothing.
        if (existingLeftCircuit == existingRightCircuit)
        {
            // noop
            return;
        }

        // Both in different existing circuits: merge
        if (existingLeftCircuit is not null && existingRightCircuit is not null)
        {
            circuits.Remove(existingLeftCircuit);
            circuits.Remove(existingRightCircuit);

            List<(int X, int Y, int Z)> newCircuit = [.. existingLeftCircuit, .. existingRightCircuit];
            circuits.Add(newCircuit);
            return;
        }

        // Left but not right: add right to left
        if (existingLeftCircuit is not null && existingRightCircuit is null)
        {
            existingLeftCircuit.Add(right);
            return;
        }

        if (existingRightCircuit is not null && existingLeftCircuit is null)
        {
            existingRightCircuit.Add(left);
            return;
        }

        // We've missed a condition
        Debug.Fail("Code fault - missed a condition");
    }
}
