namespace AdventOfCode.Year2022.Day13
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;
    using static AdventOfCode.Helpers.StringExtensions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            StringExtensions.LineSplitEnumerator enumerator = input.SplitLines();

            int index = 1;
            int sumOfCorrectlyOrderedPacketIndices = 0;

            do
            {
                enumerator.MoveNext();
                ReadOnlySpan<char> left = enumerator.Current.Line;

                enumerator.MoveNext();
                ReadOnlySpan<char> right = enumerator.Current.Line;

                if (PacketComparer.ComparePackets(left, right) == PacketTokenComparisonResult.Correct)
                {
                    sumOfCorrectlyOrderedPacketIndices += index;
                }

                ++index;
            }
            while (enumerator.MoveNext());

            return sumOfCorrectlyOrderedPacketIndices.ToString();
        }
    }
}
