namespace AdventOfCode.Year2024.Day01;

using System;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        Span<int> left = stackalloc int[input.Length];
        Span<int> right = stackalloc int[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            int length = input[i].Length / 2;
            left[i] = int.Parse(input[i].AsSpan(0, length));
            right[i] = int.Parse(input[i].AsSpan(length));
        }

        int totalSimilarity = 0;

        for (int i = 0; i < input.Length; i++)
        {
            int target = left[i];

            int occurrences = 0;
            for (int j = 0; j < input.Length; j++)
            {
                if (right[j] == target)
                {
                    occurrences++;
                }
            }

            totalSimilarity += target * occurrences;
        }

        return totalSimilarity.ToString();
    }
}
