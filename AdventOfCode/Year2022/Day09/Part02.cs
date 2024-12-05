namespace AdventOfCode.Year2022.Day09;

using System.Collections.Generic;
using AdventOfCode;

public class Part02 : ISolution
{
    private static readonly Dictionary<char, (int X, int Y)> Directions = new()
    {
        { 'U', (0, 1) },
        { 'D', (0, -1) },
        { 'L', (-1, 0) },
        { 'R', (1, 0) },
    };

    public string Solve(string[] input)
    {
        return KnotSimulation.Run(input, 10);
    }
}
