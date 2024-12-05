namespace AdventOfCode.Year2017.Day02;

using System;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int checksumTotal = 0;

        foreach (string row in input)
        {
            checksumTotal += CalculateChecksum(row);
        }

        return checksumTotal.ToString();
    }

    private static int CalculateChecksum(ReadOnlySpan<char> line)
    {
        ushort min = ushort.MaxValue;
        ushort max = ushort.MinValue;

        foreach (StringExtensions.StringSplitEntry element in line.OptimizedSplit("\t"))
        {
            ushort val = ushort.Parse(element.Line);
            if (val < min)
            {
                min = val;
            }

            if (val > max)
            {
                max = val;
            }
        }

        return max - min;
    }
}
