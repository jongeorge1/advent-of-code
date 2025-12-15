namespace AdventOfCode.Helpers;

using System.Collections.Generic;

public class CoordinateCompressor
{
    public static CompressedCoordinatesResult CompressCoordinates((long X, long Y)[] coordinates)
    {
        var xSet = new SortedSet<long>();
        var ySet = new SortedSet<long>();

        foreach ((long x, long y) in coordinates)
        {
            xSet.Add(x);
            ySet.Add(y);
        }

        var xMapping = new Dictionary<long, int>();
        var yMapping = new Dictionary<long, int>();
        int index = 0;

        foreach (long x in xSet)
        {
            xMapping[x] = index++;
        }

        index = 0;
        foreach (long y in ySet)
        {
            yMapping[y] = index++;
        }

        var compressedCoordinates = new (int X, int Y)[coordinates.Length];

        for (int i = 0; i < coordinates.Length; i++)
        {
            (long x, long y) = coordinates[i];
            compressedCoordinates[i] = (xMapping[x], yMapping[y]);
        }

        return new CompressedCoordinatesResult(
            coordinates,
            compressedCoordinates,
            xMapping,
            yMapping);
    }

    public record CompressedCoordinatesResult(
        (long X, long Y)[] OriginalCoordinates,
        (int X, int Y)[] CompressedCoordinates,
        Dictionary<long, int> XMapping,
        Dictionary<long, int> YMapping)
    {
    }
}
