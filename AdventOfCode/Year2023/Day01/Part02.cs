namespace AdventOfCode.Year2023.Day01
{
    using System;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private static (string SearchString, int Number)[] numbers =
            [
                ("1", 1),
                ("2", 2),
                ("3", 3),
                ("4", 4),
                ("5", 5),
                ("6", 6),
                ("7", 7),
                ("8", 8),
                ("9", 9),
                ("one", 1),
                ("two", 2),
                ("three", 3),
                ("four", 4),
                ("five", 5),
                ("six", 6),
                ("seven", 7),
                ("eight", 8),
                ("nine", 9),
            ];

        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string entry in input)
            {
                ReadOnlySpan<char> entrySpan = entry.AsSpan();

                int digit = FindFirstDigitInSpan(entrySpan, 0, 1);
                runningTotal += digit * 10;

                digit = FindFirstDigitInSpan(entrySpan, entrySpan.Length - 1, -1);
                runningTotal += digit;
            }

            return runningTotal.ToString();
        }

        private static int FindFirstDigitInSpan(ReadOnlySpan<char> span, int startIndex, int direction)
        {
            for (int i = startIndex; i < span.Length; i += direction)
            {
                foreach ((string SearchString, int Number) number in numbers)
                {
                    if (i + number.SearchString.Length <= span.Length && span[i..].StartsWith(number.SearchString))
                    {
                        return number.Number;
                    }
                }
            }

            throw new Exception("No digit found in span");
        }
    }
}
