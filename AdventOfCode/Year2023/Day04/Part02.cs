namespace AdventOfCode.Year2023.Day04
{
    using System;
    using System.Diagnostics;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            bool testMode = input.Length == 6;
            int winningNumbersStartColumn = testMode ? 7 : 9;
            int winningNumbersCount = testMode ? 5 : 10;
            int actualNumbersStartColumn = testMode ? 24 : 41;

            int[] scratchCardCounts = new int[input.Length];
            int runningTotal = 0;

            for (int lineIndex = 0; lineIndex < input.Length; ++lineIndex)
            {
                // We didn't initialise the array, so we need to add one to the count for the current line.
                scratchCardCounts[lineIndex] += 1;

                Debug.Assert(scratchCardCounts[lineIndex] > 0);

                runningTotal += scratchCardCounts[lineIndex];

                int score = ScoreScratchcard(ref input[lineIndex], winningNumbersStartColumn, winningNumbersCount, actualNumbersStartColumn);

                for (int nextCardIndex = 0; nextCardIndex < score; ++nextCardIndex)
                {
                    int targetIndex = lineIndex + nextCardIndex + 1;
                    if (targetIndex >= scratchCardCounts.Length)
                    {
                        break;
                    }

                    scratchCardCounts[lineIndex + nextCardIndex + 1] += scratchCardCounts[lineIndex];
                }
            }

            return runningTotal.ToString();
        }

        private static int ScoreScratchcard(ref string card, int winningNumbersStartColumn, int winningNumbersCount, int actualNumbersStartColumn)
        {
            ReadOnlySpan<char> row = card.AsSpan();
            ReadOnlySpan<char> winningNumberStrip = row[winningNumbersStartColumn..(actualNumbersStartColumn - 2)];
            ReadOnlySpan<char> actualNumberStrip = row[actualNumbersStartColumn..];

            int winningNumberCount = 0;

            for (int winningNumberIndex = 0; winningNumberIndex < winningNumbersCount; ++winningNumberIndex)
            {
                int startIndex = winningNumberIndex * 3;
                ReadOnlySpan<char> winningNumber = winningNumberStrip[startIndex..(startIndex + 3)];
                if (actualNumberStrip.Contains(winningNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    ++winningNumberCount;
                }
            }

            return winningNumberCount;
        }
    }
}
