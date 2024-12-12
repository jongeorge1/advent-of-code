namespace AdventOfCode.Year2024.Day12;

using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        var garden = Garden.Create(input);

        int totalCost = garden.Regions.Sum(region => region.GetArea() * region.GetEdgeCount());
        return totalCost.ToString();
    }
}
