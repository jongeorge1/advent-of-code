namespace AdventOfCode.Year2024.Day12;

using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        var garden = Garden.Create(input);

        int totalCost = garden.Regions.Sum(region => region.GetArea() * region.GetPerimeter());
        return totalCost.ToString();
    }
}
