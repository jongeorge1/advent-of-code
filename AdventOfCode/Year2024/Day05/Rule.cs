namespace AdventOfCode.Year2024.Day05;

using System;

public readonly record struct Rule
{
    public Rule(ReadOnlySpan<char> rule)
    {
        int splitIndex = rule.IndexOf("|");
        this.First = int.Parse(rule[..splitIndex]);
        this.Second = int.Parse(rule[(splitIndex + 1) ..]);
    }

    public readonly int First { get; }

    public readonly int Second { get; }
}
