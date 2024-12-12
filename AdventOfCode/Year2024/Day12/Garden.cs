namespace AdventOfCode.Year2024.Day12;

using System.Collections.Generic;
using System.Collections.Immutable;
using AdventOfCode.Helpers;

public class Garden
{
    private Garden()
    {
    }

    public required ImmutableList<Region> Regions { get; init; }

    public static Garden Create(string[] input)
    {
        var garden = Map<char>.CreateCharMap(input);

        List<Region> regions = [];
        HashSet<(int X, int Y)> processedLocations = [];

        foreach (((int X, int Y) location, char plant) in garden)
        {
            if (processedLocations.Contains(location))
            {
                continue;
            }

            (int X, int Y)[] newRegion = GetAllPlotsInRegion(garden, location, plant);
            regions.Add(new Region(plant, newRegion));
            processedLocations.UnionWith(newRegion);
        }

        return new Garden { Regions = [.. regions] };
    }

    private static (int X, int Y)[] GetAllPlotsInRegion(Map<char> garden, (int X, int Y) location, char plant)
    {
        List<(int X, int Y)> region = new();
        ExpandRegionFromPlot(garden, location, plant, region);
        return [.. region];
    }

    private static void ExpandRegionFromPlot(Map<char> garden, (int X, int Y) location, char plant, List<(int X, int Y)> region)
    {
        // If the location is out of bounds, contains a different plant, or is already in the region, return an empty list.
        if (!garden.IsLocationInBounds(location) || garden[location] != plant || region.Contains(location))
        {
            return;
        }

        // Otherwise, this plot is part of the region, so add it.
        region.Add(location);

        // Now add any valid touching plots.
        foreach (Direction2D direction in Direction2D.All)
        {
            ExpandRegionFromPlot(garden, direction.GetNextLocation(location), plant, region);
        }
    }
}
