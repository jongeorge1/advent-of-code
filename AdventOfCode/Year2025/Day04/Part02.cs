namespace AdventOfCode.Year2025.Day04;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        List<(int X, int Y)> rollLocations = ParseInput(input);
        int totalRemovedRolls = 0;
        int removedRolls = 0;

        do
        {
            List<(int X, int Y)> inaccessibleRolls = [.. rollLocations.Where(x => !IsAccessible(x, rollLocations))];
            removedRolls = rollLocations.Count - inaccessibleRolls.Count;
            rollLocations = inaccessibleRolls;
            totalRemovedRolls += removedRolls;
        }
        while (removedRolls > 0);

        return totalRemovedRolls.ToString();
    }

    private static bool IsAccessible((int X, int Y) location, List<(int X, int Y)> rollLocations)
    {
        int fullSpaces = 0;
        foreach (Direction2D direction in Direction2D.AllWithDiagonals)
        {
            if (rollLocations.Contains(direction.GetNextLocation(location)))
            {
                fullSpaces++;
            }

            if (fullSpaces > 3)
            {
                return false;
            }
        }

        return true;
    }

    private static List<(int X, int Y)> ParseInput(string[] input)
    {
        List<(int X, int Y)> rollLocations = [];

        for (int y = 0; y < input.Length; y++)
        {
            string line = input[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '@')
                {
                    rollLocations.Add((x, y));
                }
            }
        }

        return rollLocations;
    }
}
