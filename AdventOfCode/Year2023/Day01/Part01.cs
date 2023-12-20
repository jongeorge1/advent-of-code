namespace AdventOfCode.Year2023.Day01
{
    using System;
    using System.Buffers;
    using System.ComponentModel.DataAnnotations;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        private static SearchValues<char> digits = SearchValues.Create("123456789");

        public string Solve(string[] input)
        {
            int runningTotal = 0;

            foreach (string entry in input)
            {
                ReadOnlySpan<char> entrySpan = entry.AsSpan();
                int index = entrySpan.IndexOfAny(digits);
                runningTotal += (entrySpan[index] - '0') * 10;

                index = entrySpan.LastIndexOfAny(digits);
                runningTotal += entrySpan[index] - '0';
            }

            // Converting the number to a string takes ~4us.
            return runningTotal.ToString();
        }
    }
}
