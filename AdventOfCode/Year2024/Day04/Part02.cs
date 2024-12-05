namespace AdventOfCode.Year2024.Day04;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    private const string Forward = "MAS";
    private const string Backward = "SAM";

    public string Solve(string[] input)
    {
        var leftCenters = new List<(int Row, int Column)>();
        leftCenters.AddRange(FindLeftToRightDiagonalCenters(input, Forward));
        leftCenters.AddRange(FindLeftToRightDiagonalCenters(input, Backward));

        var rightCenters = new List<(int Row, int Column)>();
        rightCenters.AddRange(FindRightToLeftDiagonalCenters(input, Forward));
        rightCenters.AddRange(FindRightToLeftDiagonalCenters(input, Backward));

        var xCenters = leftCenters.Intersect(rightCenters).ToList();
        return xCenters.Count.ToString();
    }

    private static IEnumerable<(int Row, int Column)> FindLeftToRightDiagonalCenters(string[] input, string word)
    {
        Debug.Assert(word.Length == 3);

        for (int column = 0; column < input[0].Length - word.Length + 1; column++)
        {
            for (int row = 0; row < input.Length - word.Length + 1; row++)
            {
                bool match = true;
                for (int character = 0; character < word.Length; character++)
                {
                    match = input[row + character][column + character] == word[character];
                    if (!match)
                    {
                        break;
                    }
                }

                if (match)
                {
                    yield return (row + 1, column + 1);
                }
            }
        }
    }

    private static IEnumerable<(int Row, int Column)> FindRightToLeftDiagonalCenters(string[] input, string word)
    {
        Debug.Assert(word.Length == 3);

        for (int column = word.Length - 1; column < input[0].Length; column++)
        {
            for (int row = 0; row < input.Length - word.Length + 1; row++)
            {
                bool match = true;
                for (int character = 0; character < word.Length; character++)
                {
                    match = input[row + character][column - character] == word[character];
                    if (!match)
                    {
                        break;
                    }
                }

                if (match)
                {
                    yield return (row + 1, column - 1);
                }
            }
        }
    }
}
