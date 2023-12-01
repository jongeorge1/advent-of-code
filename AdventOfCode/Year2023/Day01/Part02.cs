namespace AdventOfCode.Year2023.Day01
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        private static string[] numbers =
            [
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
            ];

        private static int[] numberSkipChars =
            [
                2,
                2,
                4,
                4,
                3,
                3,
                4,
                4,
                3,
            ];

        public string Solve(string input)
        {
            int runningTotal = 0;

            int currentFirstDigit = -1;
            int currentLastDigit = 0;

            foreach (StringExtensions.StringSplitEntry entry in input.OptimizedSplit(Environment.NewLine.AsSpan()))
            {
                for (int i = 0; i < entry.Line.Length; i++)
                {
                    if (char.IsDigit(entry.Line[i]))
                    {
                        currentLastDigit = entry.Line[i] - '0';

                        if (currentFirstDigit == -1)
                        {
                            currentFirstDigit = currentLastDigit;
                        }
                    }
                    else
                    {
                        for (int word = 0; word < numbers.Length; word++)
                        {
                            if (i + numbers[word].Length <= entry.Line.Length && entry.Line[i..].StartsWith(numbers[word]))
                            {
                                currentLastDigit = word + 1;

                                if (currentFirstDigit == -1)
                                {
                                    currentFirstDigit = currentLastDigit;
                                }

                                i += numberSkipChars[word] - 1;
                                break;
                            }
                        }
                    }
                }

                runningTotal += currentFirstDigit * 10;
                runningTotal += currentLastDigit;

                currentFirstDigit = -1;
            }

            return runningTotal.ToString();
        }
    }
}
