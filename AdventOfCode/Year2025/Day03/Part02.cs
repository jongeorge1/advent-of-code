namespace AdventOfCode.Year2025.Day03;

using System;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        long combinedJoltage = 0;

        foreach (string bank in input)
        {
            combinedJoltage += CalculateMaximumJoltage(bank);
        }

        return combinedJoltage.ToString();
    }

    private static long CalculateMaximumJoltage(string bank)
    {
        int remainingDigits = 12;
        long joltage = 0;
        char[] remainingBatteries = bank.ToCharArray();

        while (remainingDigits > 0)
        {
            // Find the highest available battery in the list, accounting for the need to retain enough batteries to fill the remaining digits.
            char[] availableBatteries = remainingBatteries[..^(remainingDigits - 1)];
            char first = availableBatteries.Max();
            int indexOfFirst = Array.IndexOf(availableBatteries, first);

            joltage += (first - '0') * (long)Math.Pow(10, remainingDigits - 1);

            remainingBatteries = remainingBatteries[(indexOfFirst + 1)..];
            --remainingDigits;
        }

        return joltage;
    }
}
