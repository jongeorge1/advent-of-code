namespace AdventOfCode.Year2023.Day01
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            int currentFirstDigit = -1;
            int currentLastDigit = 0;

            foreach (string entry in input)
            {
                // Iterating over each entry takes an additional ~4us.
                for (int i = 0; i < entry.Length; i++)
                {
                    // This check takes ~0.7us.
                    if (char.IsDigit(entry[i]))
                    {
                        // This code takes around ~5us.
                        currentLastDigit = entry[i] - '0';

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
