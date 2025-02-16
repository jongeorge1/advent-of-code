namespace AdventOfCode.Year2024.Day21;

using System.Collections.Generic;

public class DoorBot : Bot
{
    private static readonly Dictionary<char, (int X, int Y)> DoorKeyPad = new()
    {
        { '7', (0, 0) },
        { '8', (1, 0) },
        { '9', (2, 0) },
        { '4', (0, 1) },
        { '5', (1, 1) },
        { '6', (2, 1) },
        { '1', (0, 2) },
        { '2', (1, 2) },
        { '3', (2, 2) },
        { '0', (1, 3) },
        { 'A', (2, 3) },
    };

    public DoorBot()
        : base(DoorKeyPad, 'A')
    {
    }
}