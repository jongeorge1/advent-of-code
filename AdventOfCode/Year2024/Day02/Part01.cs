namespace AdventOfCode.Year2024.Day02;

using System;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int safeCount = input.Count(IsSafe);
        return safeCount.ToString();
    }

    private static bool IsSafe(string report)
    {
        bool ascending = true;
        int[] levels = report.Split(' ').Select(int.Parse).ToArray();

        if (levels[0] > levels[1])
        {
            ascending = false;
        }

        for (int i = 1; i < levels.Length; i++)
        {
            int difference = levels[i] - levels[i - 1];

            if (ascending && !(difference >= 1 && difference <= 3))
            {
                return false;
            }

            if (!ascending && !(difference <= -1 && difference >= -3))
            {
                return false;
            }
        }

        return true;
    }
}
