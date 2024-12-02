namespace AdventOfCode.Year2024.Day02;

using System;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        int safeCount = input.Count(IsSafeWithProblemDampener);
        return safeCount.ToString();
    }

    private static bool IsSafeWithProblemDampener(string report)
    {
        int[] levels = report.Split(' ').Select(int.Parse).ToArray();

        if (IsSafe(levels))
        {
            return true;
        }

        for (int i = 0; i < levels.Length; ++i)
        {
            int[] newReport = RemoveElementAt(levels, i);
            if (IsSafe(newReport))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsSafe(int[] levels)
    {
        bool ascending = true;

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

    private static int[] RemoveElementAt(int[] array, int index)
    {
        if (index == 0)
        {
            return array[1..];
        }

        if (index == array.Length - 1)
        {
            return array[..^1];
        }

        int[] result = new int[array.Length - 1];
        Array.Copy(array, 0, result, 0, index);
        Array.Copy(array, index + 1, result, index, array.Length - index - 1);
        return result;
    }
}
