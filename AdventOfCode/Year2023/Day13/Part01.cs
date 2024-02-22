namespace AdventOfCode.Year2023.Day13
{
    using System;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int currentPatternStart = 0;
            int runningTotal = 0;

            do
            {
                int currentPatternEndExclusive = FindCurrentPatternEndExclusive(input, currentPatternStart);

                // For ease of comparison and rotation, we'll turn the row into an array of integers
                int[] grid = ParseGrid(input, currentPatternStart, currentPatternEndExclusive);

                // First evaluate rows. We're looking for two rows that match exactly, so we'll start
                // on the second row in the current pattern and compare against previous rows.
                int lineIndex = TryFindHorizontalReflectionIndex(grid);

                if (lineIndex > 0)
                {
                    // We've found a horizontal reflection line;
                    runningTotal += 100 * lineIndex;
                }
                else
                {
                    // No horizontal reflection. Rotate the grid and try again.
                    int[] rotatedGrid = RotateGridClockwise(grid, input[currentPatternStart].Length);

                    // Now use the same logic
                    lineIndex = TryFindHorizontalReflectionIndex(rotatedGrid);
                    if (lineIndex > 0)
                    {
                        runningTotal += lineIndex;
                    }
                    else
                    {
                        // If we're here, we didn't find a reflection line. This is wrong.
                        throw new Exception("Didn't find a reflection line");
                    }
                }

                currentPatternStart = currentPatternEndExclusive + 1;
            }
            while (currentPatternStart < input.Length);

            return runningTotal.ToString();
        }

        private static int TryFindHorizontalReflectionIndex(int[] grid)
        {
            for (int i = 1; i < grid.Length; i++)
            {
                if (IsHorizontalReflectionLine(grid, i))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool IsHorizontalReflectionLine(int[] grid, int index)
        {
            // A line is reflection line if all available rows on both sides match
            int reflectionStart = index - 1;
            int reflectionEnd = index;
            while (reflectionStart >= 0 && reflectionEnd < grid.Length)
            {
                if (grid[reflectionStart] != grid[reflectionEnd])
                {
                    return false;
                }

                --reflectionStart;
                ++reflectionEnd;
            }

            return true;
        }

        private static int FindCurrentPatternEndExclusive(string[] input, int currentPatternStart)
        {
            // Find the row on which the current pattern ends.
            int currentPatternEndExclusive = currentPatternStart;
            while (currentPatternEndExclusive != input.Length && input[currentPatternEndExclusive] != string.Empty)
            {
                ++currentPatternEndExclusive;
            }

            return currentPatternEndExclusive;
        }

        private static int[] ParseGrid(string[] input, int currentPatternStart, int currentPatternEndExclusive)
        {
            int[] parsedRows = new int[currentPatternEndExclusive - currentPatternStart];

            for (int row = currentPatternStart; row < currentPatternEndExclusive; ++row)
            {
                for (int i = 0; i < input[row].Length; ++i)
                {
                    if (input[row][i] == '#')
                    {
                        parsedRows[row - currentPatternStart] += (int)Math.Pow(2, input[row].Length - i - 1);
                    }
                }
            }

            return parsedRows;
        }

        private int[] RotateGridClockwise(int[] grid, int width)
        {
            int[] newRows = new int[width];

            for (int row = 0; row < grid.Length; ++row)
            {
                for (int column = 0; column < width; ++column)
                {
                    if ((grid[row] & (int)Math.Pow(2, width - column - 1)) != 0)
                    {
                        newRows[column] = newRows[column] + (int)Math.Pow(2, row);
                    }
                }
            }

            return newRows;
        }
    }
}
