namespace AdventOfCode.Year2023.Day01
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int runningTotal = 0;

            int currentFirstDigit = -1;
            int currentLastDigit = 0;

            // Iterating over the input takes ~5.4us.
            foreach (StringExtensions.StringSplitEntry entry in input.OptimizedSplit(Environment.NewLine.AsSpan()))
            {
                // Iterating over each entry takes an additional ~4us.
                for (int i = 0; i < entry.Line.Length; i++)
                {
                    // This check takes ~0.7us.
                    if (char.IsDigit(entry.Line[i]))
                    {
                        // This code takes around ~5us.
                        currentLastDigit = entry.Line[i] - '0';

                        if (currentFirstDigit == -1)
                        {
                            currentFirstDigit = currentLastDigit;
                        }
                    }
                }

                // The next three lines account for around 1us.
                runningTotal += currentFirstDigit * 10;
                runningTotal += currentLastDigit;

                currentFirstDigit = -1;
            }

            // Converting the number to a string takes ~4us.
            return runningTotal.ToString();
        }
    }
}
