namespace AdventOfCode.Year2024.Day21;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int totalComplexity = 0;

        for (int i = 0; i < input.Length; ++i)
        {
            totalComplexity += GetComplexity(input[i]);
        }

        return totalComplexity.ToString();
    }

    private static int GetComplexity(string code)
    {
        int shortestButtonPressSequenceLength = GetShortestButtonPressSequenceLength(code);
        int codeNumber = int.Parse(code[..^1]);

        return codeNumber * shortestButtonPressSequenceLength;
    }

    private static int GetShortestButtonPressSequenceLength(string code)
    {
        string[] sequences = GetPossibleDoorPadButtonPressSequences(code);
        sequences = [..sequences.SelectMany(s => GetPossibleDirectionPadButtonPressSequences(s))];
        sequences = [.. sequences.SelectMany(s => GetPossibleDirectionPadButtonPressSequences(s))];
        return sequences.Min(x => x.Length);
    }

    private static string[] GetPossibleDoorPadButtonPressSequences(string code)
    {
        return GetPossibleButtonPressSequences(code, Keypads.NumericPadShortestRoutes);
    }

    private static string[] GetPossibleDirectionPadButtonPressSequences(string code)
    {
        return GetPossibleButtonPressSequences(code, Keypads.DirectionPadShortestRoutes);
    }

    private static string[] GetPossibleButtonPressSequences(string code, Dictionary<(char From, char To), string[]> padRoutes)
    {
        char currentPosition = 'A';
        List<string> possibleFullRoutes = [string.Empty];
        for (int i = 0; i < code.Length; ++i)
        {
            char targetPosition = code[i];

            string[] possibleRoutes =
                currentPosition == targetPosition
                ? [string.Empty]
                : padRoutes[(currentPosition, targetPosition)];

            List<string> combinedRoutes = [];
            foreach (string current in possibleFullRoutes)
            {
                foreach (string newRoute in possibleRoutes)
                {
                    combinedRoutes.Add(current + newRoute + 'A');
                }
            }

            possibleFullRoutes = combinedRoutes;
            currentPosition = targetPosition;
        }

        return [.. possibleFullRoutes];
    }
}
