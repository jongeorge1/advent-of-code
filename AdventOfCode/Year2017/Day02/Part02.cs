using System;
using AdventOfCode.Helpers;

namespace AdventOfCode.Year2017.Day02
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int checksumTotal = 0;

            foreach (string row in input)
            {
                checksumTotal += CalculateChecksum(row);
            }

            return checksumTotal.ToString();
        }

        private static int CalculateChecksum(ReadOnlySpan<char> line)
        {
            Span<ushort> numbers = stackalloc ushort[16];
            int written = 0;

            foreach (StringExtensions.StringSplitEntry element in line.OptimizedSplit("\t"))
            {
                numbers[written++] = ushort.Parse(element.Line);
            }

            numbers = numbers[..written];

            // Now look for the two that divide equally...
            for (byte current = 0; current < written; ++current)
            {
                for (byte target = 0; target < written; ++target)
                {
                    if (current == target)
                    {
                        continue;
                    }

                    if (numbers[current] % numbers[target] == 0)
                    {
                        return numbers[current] / numbers[target];
                    }
                }
            }

            throw new Exception();
        }
    }
}
