namespace AdventOfCode.Year2025.Day09;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        (long X, long Y)[] coordinates = [.. input.Select(row =>
        {
            long[] parts = [.. row.Split(',').Select(long.Parse)];
            return (parts[0], parts[1]);
        })];

        long largestArea = 0;

        for (int leftIndex = 0; leftIndex < coordinates.Length; leftIndex++)
        {
            for (int rightIndex = leftIndex + 1; rightIndex < coordinates.Length; rightIndex++)
            {
                if (rightIndex <= leftIndex)
                {
                    continue;
                }

                (long X, long Y) left = coordinates[leftIndex];
                (long X, long Y) right = coordinates[rightIndex];

                long width = Math.Abs(right.X - left.X) + 1;
                long height = Math.Abs(right.Y - left.Y) + 1;

                long area = width * height;

                largestArea = Math.Max(area, largestArea);
            }
        }

        return largestArea.ToString();
    }
}
