﻿namespace AdventOfCode.Year2022.Day13
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private const string FirstDividerPacket = "[[2]]";
        private const string SecondDividerPacket = "[[6]]";

        private const string DividerPackets = $"\r\n{FirstDividerPacket}\r\n{SecondDividerPacket}";

        public string Solve(string[] input)
        {
            var fullInput = input.ToList();
            fullInput.Add(FirstDividerPacket);
            fullInput.Add(SecondDividerPacket);

            fullInput.Sort((string left, string right) =>
            {
                PacketTokenComparisonResult comparisonResult = PacketComparer.ComparePackets(left.AsSpan(), right.AsSpan());
                return comparisonResult switch
                {
                    PacketTokenComparisonResult.Correct => -1,
                    PacketTokenComparisonResult.Incorrect => 1,
                    _ => 0,
                };
            });

            int indexOfFirstDivider = fullInput.IndexOf(FirstDividerPacket) + 1;
            int indexOfSecondDivider = fullInput.IndexOf(SecondDividerPacket) + 1;

            return (indexOfFirstDivider * indexOfSecondDivider).ToString();
        }
    }
}
