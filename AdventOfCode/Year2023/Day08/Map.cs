namespace AdventOfCode.Year2023.Day08;

using System.Collections.Generic;

public readonly record struct Map(string Descriptor)
{
    public string Location { get; } = Descriptor[0..3];

    public Dictionary<char, string> Directions { get; } = new()
    {
        ['L'] = Descriptor[7..10],
        ['R'] = Descriptor[12..15],
    };
}