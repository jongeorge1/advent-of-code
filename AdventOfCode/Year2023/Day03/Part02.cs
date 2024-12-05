namespace AdventOfCode.Year2023.Day03;

using System;
using System.Buffers;
using AdventOfCode;

public class Part02 : ISolution
{
    private static SearchValues<char> digits = SearchValues.Create("0123456789");
    private static SearchValues<char> thingsThatAreNotAGear = SearchValues.Create("0123456789.");

    [Flags]
    private enum NumberLocations
    {
        AboveLeft = 1 << 0,
        AboveCenter = 1 << 1,
        AboveRight = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4,
        BelowLeft = 1 << 5,
        BelowCenter = 1 << 6,
        BelowRight = 1 << 7,
    }

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

        int indexOfCurrentGear = line.IndexOfAnyExcept(thingsThatAreNotAGear);
        while (indexOfCurrentGear != -1)
        {
            // First determine how many numbers there are around the current gear and stash their locations; we'll only parse
            // them if we need to.
            // ASSUMPTION: There are no gears adjacent to each other.
            int foundNumbersCount = 0;
            var foundNumbers = new (int row, int minimumInclusiveStartColumn, int maximumExclusiveEndColumn, bool trimFromStart, bool trimFromEnd)[3];

            // Test row above
            if (lineIndex > 0)
            {
                if (input[lineIndex - 1][indexOfCurrentGear] != '.')
                {
                    foundNumbers[foundNumbersCount] = (lineIndex - 1, indexOfCurrentGear - 2, indexOfCurrentGear + 3, true, true);
                    ++foundNumbersCount;
                }
                else
                {
                    if (indexOfCurrentGear > 0 && input[lineIndex - 1][indexOfCurrentGear - 1] != '.')
                    {
                        foundNumbers[foundNumbersCount] = (lineIndex - 1, indexOfCurrentGear - 3, indexOfCurrentGear, true, false);
                        ++foundNumbersCount;
                    }

                    if (indexOfCurrentGear < line.Length - 1 && input[lineIndex - 1][indexOfCurrentGear + 1] != '.')
                    {
                        foundNumbers[foundNumbersCount] = (lineIndex - 1, indexOfCurrentGear + 1, indexOfCurrentGear + 4, false, true);
                        ++foundNumbersCount;
                    }
                }
            }

            // We can have a max of two numbers at this point, so no short-circuiting yet.
            if (indexOfCurrentGear > 0 && line[indexOfCurrentGear - 1] != '.')
            {
                foundNumbers[foundNumbersCount] = (lineIndex, indexOfCurrentGear - 3, indexOfCurrentGear, true, false);
                ++foundNumbersCount;
            }

            // Now we could have three numbers, so we can short-circuit.
            if (foundNumbersCount < 3 && indexOfCurrentGear < line.Length - 1 && line[indexOfCurrentGear + 1] != '.')
            {
                foundNumbers[foundNumbersCount] = (lineIndex, indexOfCurrentGear + 1, indexOfCurrentGear + 4, false, true);
                ++foundNumbersCount;
            }

            // Now test the row below
            if (foundNumbersCount < 3 && lineIndex < (input.Length - 1))
            {
                if (input[lineIndex + 1][indexOfCurrentGear] != '.')
                {
                    foundNumbers[foundNumbersCount] = (lineIndex + 1, indexOfCurrentGear - 2, indexOfCurrentGear + 3, true, true);
                    ++foundNumbersCount;
                }
                else
                {
                    if (indexOfCurrentGear > 0 && input[lineIndex + 1][indexOfCurrentGear - 1] != '.')
                    {
                        foundNumbers[foundNumbersCount] = (lineIndex + 1, indexOfCurrentGear - 3, indexOfCurrentGear, true, false);
                        ++foundNumbersCount;
                    }

                    if (indexOfCurrentGear < line.Length - 1 && input[lineIndex + 1][indexOfCurrentGear + 1] != '.')
                    {
                        foundNumbers[foundNumbersCount] = (lineIndex + 1, indexOfCurrentGear + 1, indexOfCurrentGear + 4, false, true);
                        ++foundNumbersCount;
                    }
                }
            }

            if (foundNumbersCount == 2)
            {
                // Console.WriteLine($"Gear found at {lineIndex},{indexOfCurrentGear}");

                // We have exactly two numbers and we know their positions, so we can parse them.
                int[] numbers = new int[2];
                for (int i = 0; i < numbers.Length; ++i)
                {
                    // For 2 digit numbers, we might have a leading or trailing . which we need to remove.
                    // For the above/below center numbers, we might have captured the end of a previous, or the
                    // start of a next number, which we also need to trim.
                    ReadOnlySpan<char> targetLine = input[foundNumbers[i].row].AsSpan();
                    int start = foundNumbers[i].minimumInclusiveStartColumn;
                    if (foundNumbers[i].trimFromStart && targetLine[start + 1] == '.')
                    {
                        start += 2;
                    }

                    while (foundNumbers[i].trimFromStart && targetLine[start] == '.')
                    {
                        ++start;
                    }

                    int end = foundNumbers[i].maximumExclusiveEndColumn;
                    if (foundNumbers[i].trimFromEnd && targetLine[end - 2] == '.')
                    {
                        end -= 2;
                    }

                    while (foundNumbers[i].trimFromEnd && targetLine[end - 1] == '.')
                    {
                        --end;
                    }

                    numbers[i] = int.Parse(targetLine[start..end]);
                }

                runningTotal += numbers[0] * numbers[1];
            }

            int searchFrom = indexOfCurrentGear + 1;
            indexOfCurrentGear = line[searchFrom..].IndexOfAnyExcept(thingsThatAreNotAGear);
            if (indexOfCurrentGear != -1)
            {
                indexOfCurrentGear += searchFrom;
            }
        }
    }
}
