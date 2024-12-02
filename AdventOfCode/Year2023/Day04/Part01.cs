namespace AdventOfCode.Year2023.Day04
{
    using System;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;
            bool testMode = input.Length == 6;
            int winningNumbersStartColumn = testMode ? 7 : 9;
            int winningNumbersCount = testMode ? 5 : 10;
            int actualNumbersStartColumn = testMode ? 24 : 41;

            for (int lineIndex = 0; lineIndex < input.Length; ++lineIndex)
            {
                runningTotal += ScoreScratchcard(ref input[lineIndex], winningNumbersStartColumn, winningNumbersCount, actualNumbersStartColumn);
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

            if (winningNumberCount > 0)
            {
                return 1 << (winningNumberCount - 1);
            }

            return 0;
        }
    }
}
