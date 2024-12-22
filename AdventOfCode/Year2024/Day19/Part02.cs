namespace AdventOfCode.Year2024.Day19;

using System;
using System.Collections.Generic;
using AdventOfCode;

public class Part02 : ISolution
{
    private static readonly Dictionary<string, long> SeenPatterns = [];

    public string Solve(string[] input)
    {
        // Available patterns on line 0, comma+space separated.
        // Required patterns on lines 2 onward, one pattern per line.
        string[] availablePatterns = input[0].Split(", ");
        long totalManufacturingOptionCount = 0;

        for (long i = 2; i < input.Length; i++)
        {
            totalManufacturingOptionCount += GetManufacturingOptionCount(input[i].AsSpan(), availablePatterns);
        }

        return totalManufacturingOptionCount.ToString();
    }

    private static long GetManufacturingOptionCount(ReadOnlySpan<char> design, string[] availablePatterns)
    {
        long manufacturingOptions = 0;

        if (SeenPatterns.TryGetValue(design.ToString(), out manufacturingOptions))
        {
            return manufacturingOptions;
        }

        foreach (string pattern in availablePatterns)
        {
            if (design.StartsWith(pattern))
            {
                // Did we make the whole design with this pattern?
                if (design.Length == pattern.Length)
                {
                    ++manufacturingOptions;
                }

                // We can make the start of the design with the current pattern. If we do, can we make the rest?
                ReadOnlySpan<char> remainingDesign = design[pattern.Length..];
                manufacturingOptions += GetManufacturingOptionCount(remainingDesign, availablePatterns);
            }
        }

        SeenPatterns[design.ToString()] = manufacturingOptions;

        return manufacturingOptions;
    }
}
