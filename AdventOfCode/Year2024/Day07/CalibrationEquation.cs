namespace AdventOfCode.Year2024.Day07;

using System;
using System.Linq;

public readonly record struct CalibrationEquation
{
    public CalibrationEquation(string input)
    {
        long[] components = input.Split([' ', ':'], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        this.TestValue = components[0];
        this.Numbers = components[1..];
    }

    public long TestValue { get; }

    public long[] Numbers { get; }
}