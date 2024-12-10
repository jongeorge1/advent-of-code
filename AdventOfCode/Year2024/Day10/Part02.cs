namespace AdventOfCode.Year2024.Day10;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using static Shared;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        var map = Map<int>.CreateDigitMap(input);

        (int X, int Y)[] trailheads = [.. map.Where(x => x.Value == 0).Select(x => x.Key)];

        int maxX = input[0].Length - 1;
        int maxY = input.Length - 1;

        Dictionary<(int X, int Y), int> scores = new();

        foreach ((int X, int Y) trailhead in trailheads)
        {
            (int X, int Y)[] accessibleTrailHeads = GetEndLocationsAccessibleFrom(trailhead, [], map);
            scores.Add(trailhead, accessibleTrailHeads.Length);
        }

        return scores.Values.Sum().ToString();
    }
}
