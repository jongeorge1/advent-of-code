namespace AdventOfCode.Year2024.Day04;

using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    private const string Forward = "XMAS";
    private const string Backward = "SAMX";

    public string Solve(string[] input)
    {
        int appearances = FindHorizontals(input, Forward);
        appearances += FindHorizontals(input, Backward);
        appearances += FindVerticals(input, Forward);
        appearances += FindVerticals(input, Backward);
        appearances += FindLeftToRightDiagonals(input, Forward);
        appearances += FindLeftToRightDiagonals(input, Backward);
        appearances += FindRightToLeftDiagonals(input, Forward);
        appearances += FindRightToLeftDiagonals(input, Backward);
        return appearances.ToString();
    }

    private static int FindHorizontals(string[] input, string word)
    {
        return input.Aggregate(0, (agg, row) => agg += FindHorizontals(row, word));
    }

    private static int FindHorizontals(string input, string word)
    {
        int appearances = 0;
        int start = input.IndexOf(word);

        while (start != -1)
        {
            ++appearances;
            start = input.IndexOf(word, start + 1);
        }

        return appearances;
    }

    private static int FindVerticals(string[] input, string word)
    {
        int appearances = 0;

        for (int column = 0; column < input[0].Length; column++)
        {
            for (int row = 0; row < input.Length - word.Length + 1; ++row)
            {
                bool match = true;
                for (int character = 0; character < word.Length; ++character)
                {
                    match = input[row + character][column] == word[character];
                    if (!match)
                    {
                        break;
                    }
                }

                if (match)
                {
                    ++appearances;
                }
            }
        }

        return appearances;
    }

    private static int FindLeftToRightDiagonals(string[] input, string word)
    {
        int appearances = 0;

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
                    ++appearances;
                }
            }
        }

        return appearances;
    }

    private static int FindRightToLeftDiagonals(string[] input, string word)
    {
        int appearances = 0;

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
                    ++appearances;
                }
            }
        }

        return appearances;
    }
}
