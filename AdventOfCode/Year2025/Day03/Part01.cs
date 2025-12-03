namespace AdventOfCode.Year2025.Day03;

using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int combinedJoltage = 0;

        foreach (string bank in input)
        {
            combinedJoltage += CalculateMaximumJoltage(bank);
        }

        return combinedJoltage.ToString();
    }

    private static int CalculateMaximumJoltage(string bank)
    {
        // Horrible linq based solution to find the two highest values

        // First, find the highest possible first value
        char[] batteries = bank.ToCharArray();

        char first = batteries[..^1].Max();
        int indexOfFirst = bank.IndexOf(first);

        char second = batteries[(indexOfFirst + 1)..].Max();

        return ((first - '0') * 10) + (second - '0');
    }
}
