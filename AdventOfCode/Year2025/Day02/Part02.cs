namespace AdventOfCode.Year2025.Day02;

using System;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
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

            // An Id is invalid if it's made up of a repeating sequence of Ids. This means the sequence can be somewhere between 1 and half the length of the Id.
            int halfLength = idAsString.Length / 2;

            for (int sequenceLength = halfLength; sequenceLength > 0; sequenceLength--)
            {
                if (IsMadeUpOfRepeatingSequence(idAsString, sequenceLength))
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

    private static bool IsMadeUpOfRepeatingSequence(ReadOnlySpan<char> id, int sequenceLength)
    {
        // The length of the Id must be a multiple of the sequence length.
        if ((id.Length % sequenceLength) != 0)
        {
            return false;
        }

        ReadOnlySpan<char> sequence = id[..sequenceLength];

        for (int index = sequenceLength; index < id.Length; index += sequenceLength)
        {
            if (!id[index..(index + sequenceLength)].SequenceEqual(sequence))
            {
                return false;
            }
        }

        ////Console.WriteLine($"Invalid: {id} = {sequence}*{id.Length / sequenceLength}");

        return true;
    }
}
