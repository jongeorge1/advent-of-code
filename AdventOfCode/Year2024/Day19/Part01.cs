namespace AdventOfCode.Year2024.Day19;

using System;
using System.Collections.Generic;
using AdventOfCode;

public class Part01 : ISolution
{
    private static readonly Dictionary<string, bool> ValidPatterns = [];

    public string Solve(string[] input)
    {
        // Available patterns on line 0, comma+space separated.
        // Required patterns on lines 2 onward, one pattern per line.
        string[] availablePatterns = input[0].Split(", ");
        int validDesigns = 0;

        for (int i = 2; i < input.Length; i++)
        {
            if (IsValidDesign(input[i].AsSpan(), availablePatterns))
            {
                validDesigns++;
            }
        }

        return validDesigns.ToString();
    }

    private static bool IsValidDesign(ReadOnlySpan<char> design, string[] availablePatterns)
    {
        if (ValidPatterns.TryGetValue(design.ToString(), out bool isValid))
        {
            return isValid;
        }

        foreach (string pattern in availablePatterns)
        {
            if (design.StartsWith(pattern))
            {
                // Did we make the whole design with this pattern?
                if (design.Length == pattern.Length)
                {
                    ValidPatterns[design.ToString()] = true;
                    return true;
                }

                // We can make the start of the design with the current pattern. If we do, can we make the rest?
                ReadOnlySpan<char> remainingDesign = design[pattern.Length..];
                if (IsValidDesign(remainingDesign, availablePatterns))
                {
                    ValidPatterns[design.ToString()] = true;
                    return true;
                }
            }
        }

        ValidPatterns[design.ToString()] = false;
        return false;
    }
}
