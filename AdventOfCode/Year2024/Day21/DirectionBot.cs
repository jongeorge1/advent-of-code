namespace AdventOfCode.Year2024.Day21;

using System.Collections.Generic;

public class DirectionBot : Bot
{
    private static readonly Dictionary<char, (int X, int Y)> DirectionalKeyPad = new()
    {
        { '^', (1, 0) },
        { 'A', (2, 0) },
        { '<', (0, 1) },
        { 'v', (1, 1) },
        { '>', (2, 1) },
    };

    public DirectionBot()
        : base(DirectionalKeyPad, 'A')
    {
    }
}
