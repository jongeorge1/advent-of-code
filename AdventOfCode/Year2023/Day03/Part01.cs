namespace AdventOfCode.Year2023.Day03
{
    using System;
    using System.Buffers;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        private static SearchValues<char> digits = SearchValues.Create("0123456789");

        public string Solve(string[] input)
        {
            int runningTotal = 0;

            for (int lineIndex = 0; lineIndex < input.Length; ++lineIndex)
            {
                this.ProcessLine(ref input, lineIndex, ref runningTotal);
            }

            return runningTotal.ToString();
        }

        private void ProcessLine(ref string[] input, int lineIndex, ref int runningTotal)
        {
            ReadOnlySpan<char> line = input[lineIndex].AsSpan();
            int indexOfCurrentNumberFirstDigit = line.IndexOfAny(digits);

            while (indexOfCurrentNumberFirstDigit != -1)
            {
                int indexOfAfterCurrentNumberLastDigit = line[indexOfCurrentNumberFirstDigit..].IndexOfAnyExcept(digits);
                if (indexOfAfterCurrentNumberLastDigit == -1)
                {
                    indexOfAfterCurrentNumberLastDigit = line.Length;
                }
                else
                {
                    indexOfAfterCurrentNumberLastDigit += indexOfCurrentNumberFirstDigit;
                }

                // To find out if it's a part number, we need to search around the number for anything that's not a .
                // ASSUMPTION: Two numbers will never be adjacent.
                bool isPartNumber = false;

                if (lineIndex > 0)
                {
                    ReadOnlySpan<char> adjacentColumnsAbove = input[lineIndex - 1][Math.Max(0, indexOfCurrentNumberFirstDigit - 1) .. (Math.Min(line.Length - 1, indexOfAfterCurrentNumberLastDigit) + 1)].AsSpan();
                    if (adjacentColumnsAbove.IndexOfAnyExcept('.') != -1)
                    {
                        isPartNumber = true;
                    }
                }

                if (!isPartNumber && lineIndex < input.Length - 1)
                {
                    ReadOnlySpan<char> adjacentColumnsBelow = input[lineIndex + 1][Math.Max(0, indexOfCurrentNumberFirstDigit - 1) .. (Math.Min(line.Length - 1, indexOfAfterCurrentNumberLastDigit) + 1)].AsSpan();
                    if (adjacentColumnsBelow.IndexOfAnyExcept('.') != -1)
                    {
                        isPartNumber = true;
                    }
                }

                if (!isPartNumber && indexOfCurrentNumberFirstDigit > 0)
                {
                    isPartNumber = line[indexOfCurrentNumberFirstDigit - 1] != '.';
                }

                if (!isPartNumber && indexOfAfterCurrentNumberLastDigit < line.Length)
                {
                    isPartNumber = line[indexOfAfterCurrentNumberLastDigit] != '.';
                }

                if (isPartNumber)
                {
                    int number = int.Parse(line[indexOfCurrentNumberFirstDigit..indexOfAfterCurrentNumberLastDigit]);
                    runningTotal += number;
                }

                indexOfCurrentNumberFirstDigit = line[indexOfAfterCurrentNumberLastDigit..].IndexOfAny(digits);
                if (indexOfCurrentNumberFirstDigit != -1)
                {
                    indexOfCurrentNumberFirstDigit += indexOfAfterCurrentNumberLastDigit;
                }
            }
        }
    }
}
