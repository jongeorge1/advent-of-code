namespace AdventOfCode.Year2024.Day21;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    const int DirectionBotCount = 2;

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
        List<string[]> sequences = GetPossibleDoorPadButtonPressSequences(code);

        Dictionary<string, List<string[]>> directionPadCache = [];

        for (int i = 0; i < DirectionBotCount; ++i)
        {
            List<string[]> expandedSequences = [];
            foreach (string[] sequence in sequences)
            {
                IEnumerable<List<string[]>> nextSequences = sequence.Select(x => GetPossibleDirectionPadButtonPressSequences(x, directionPadCache));
                ////expandedSequences.AddRange(nextSequences);
            }

            sequences = expandedSequences;
        }

        return sequences.Min(x => x.Length);
    }

    private static List<string[]> GetPossibleDoorPadButtonPressSequences(string code)
    {
        return GetPossibleButtonPressSequences(code, Keypads.NumericPadShortestRoutes, []);
    }

    private static List<string[]> GetPossibleDirectionPadButtonPressSequences(string code, Dictionary<string, List<string[]>> directionPadCache)
    {
        return GetPossibleButtonPressSequences(code, Keypads.DirectionPadShortestRoutes, directionPadCache);
    }

    private static List<string[]> GetPossibleButtonPressSequences(string code, Dictionary<(char From, char To), string[]> padRoutes, Dictionary<string, List<string[]>> cache)
    {
        if (cache.TryGetValue(code, out List<string[]>? cachedResult))
        {
            return cachedResult;
        }

        char currentPosition = 'A';
        List<string[]> possibleFullRoutes = [];

        for (int i = 0; i < code.Length; ++i)
        {
            char targetPosition = code[i];

            string[] possibleRoutes =
                currentPosition == targetPosition
                ? [string.Empty]
                : padRoutes[(currentPosition, targetPosition)];

            possibleFullRoutes.Add([.. possibleRoutes.Select(x => x + 'A')]);

            currentPosition = targetPosition;
        }

        cache.Add(code, possibleFullRoutes);
        return possibleFullRoutes;
    }
}
