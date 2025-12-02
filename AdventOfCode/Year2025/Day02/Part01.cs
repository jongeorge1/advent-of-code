namespace AdventOfCode.Year2025.Day02;

using System;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        long total = 0;

        // There's only one line of input, and this puzzle is custom built for using spans.
        foreach (StringExtensions.StringSplitEntry range in input[0].OptimizedSplit(","))
        {
            // Now extract the start and end numbers for the range.
            int delimeterIndex = range.Line.IndexOf('-');
            long start = long.Parse(range.Line[..delimeterIndex]);
            long end = long.Parse(range.Line[(delimeterIndex + 1)..]);

            for (long current = start; current <= end; ++current)
            {
                if (!IsValidId(current))
                {
                    total += current;
                }
            }
        }

        return total.ToString();
    }

    private static bool IsValidId(long id)
    {
        Span<char> buffer = stackalloc char[20];

        if (id.TryFormat(buffer, out int charsWritten))
        {
            ReadOnlySpan<char> idAsString = buffer[..charsWritten];

            // An Id is invalid if it's got an even number of digits and the first half is the same as the second half.
            if ((idAsString.Length % 2) == 0)
            {
                int halfLength = idAsString.Length / 2;
                if (idAsString[..halfLength].SequenceEqual(idAsString.Slice(halfLength, halfLength)))
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            throw new InvalidOperationException("Unable to convert long to ReadOnlySpan<char>");
        }
    }
}
