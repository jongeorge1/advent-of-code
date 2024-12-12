namespace AdventOfCode.Year2024.Day12;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventOfCode.Helpers;

public class Region(char plant, (int X, int Y)[] plots)
{
    public char Plant { get; } = plant;

    public ImmutableArray<(int X, int Y)> Plots { get; } = [.. plots];

    public int GetArea() => this.Plots.Length;

    public int GetPerimeter() =>
        Direction2D.All.Sum(direction => this.Plots.Count(plot => !this.Plots.Contains(direction.GetNextLocation(plot))));

    public int GetEdgeCount() =>
        Direction2D.All.Sum(
            direction => CountEdgesFormedBy(
                this.Plots.Where(plot => !this.Plots.Contains(direction.GetNextLocation(plot))).ToArray(),
                direction.PerpendicularOrientation));

    private static int CountEdgesFormedBy((int X, int Y)[] edgePlots, Direction2D.Orientations orientation)
    {
        HashSet<(int X, int Y)> countedPlots = [];
        int distinctEdgesFound = 0;

        foreach ((int X, int Y) plot in edgePlots)
        {
            if (countedPlots.Contains(plot))
            {
                continue;
            }

            // Expand in each direction until we find the end of the edge that this plot is part of.
            foreach (Direction2D direction in Direction2D.AllByOrientation[orientation])
            {
                (int X, int Y) currentPlot = plot;

                while (edgePlots.Contains(currentPlot))
                {
                    countedPlots.Add(currentPlot);
                    currentPlot = direction.GetNextLocation(currentPlot);
                }
            }

            ++distinctEdgesFound;
        }

        return distinctEdgesFound;
    }
}
