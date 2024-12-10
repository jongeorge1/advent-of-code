namespace AdventOfCode.Year2024.Day10;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Helpers;

public static class Shared
{
    public static (int X, int Y)[] GetEndLocationsAccessibleFrom((int X, int Y) location, (int X, int Y)[] visitedLocations, Map<int> map)
    {
        if (map[location] == 9)
        {
            return [location];
        }

        IEnumerable<(int X, int Y)> possibleNextLocations = Direction2D.All.Select(direction => direction.GetNextLocation(location)).Where(nextLocation => map.IsLocationInBounds(nextLocation) && map[nextLocation] == map[location] + 1 && !visitedLocations.Contains(nextLocation));
        (int X, int Y)[] nextVisitedLocations = [.. visitedLocations, location];

        return possibleNextLocations.SelectMany(nextLocation => GetEndLocationsAccessibleFrom(nextLocation, nextVisitedLocations, map)).ToArray();
    }
}
