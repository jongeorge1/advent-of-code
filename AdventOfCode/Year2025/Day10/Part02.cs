namespace AdventOfCode.Year2025.Day10;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class Part02 : ISolution
{
    const int BitsPerNumber = 5;

    public string Solve(string[] input)
    {
        int totalPresses = 0;

        foreach (string row in input)
        {
            Machine machine = new(row);
            int minimumPresses = machine.CountButtonPressesToTargetJoltage();
            totalPresses += minimumPresses;
        }

        return totalPresses.ToString();
    }

    private record Machine
    {
        public Machine(string input)
        {
            string[] components = input.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            this.Target = ParseTarget(components[^1]);
            ////this.Buttons = [.. components[1..^1].Select(c => ParseButtons(c, this.Target.Length))];
        }

        public long Target { get; }

        public long[] Buttons { get; }

        public int CountButtonPressesToTargetJoltage()
        {
            ////int[] current = new int[this.Target.Length];
            ////List<int[]> seen = [];
            ////Queue<(int[] Current, int PressesSoFar)> states = [];
            ////states.Enqueue((current, 0));

            ////while (states.TryDequeue(out (int[] Current, int PressesSoFar) result))
            ////{

            ////    if (result.Current.IsIdenticalTo(this.Target))
            ////    {
            ////        return result.PressesSoFar;
            ////    }

            ////    if (seen.Contains(result.Current))
            ////    {
            ////        continue;
            ////    }

            ////    seen.Add(result.Current);

            ////    ////foreach (int button in this.Buttons)
            ////    ////{
            ////    ////    states.Enqueue((result.Current ^ button, result.PressesSoFar + 1));
            ////    ////}
            ////}

            ////Assert.Fail("Couldn't find combination");
            return -1;
        }

        private static long ParseTarget(ReadOnlySpan<char> input)
        {
            input = input[1..^1];

            List<int> result = [];

            foreach (Range current in input.Split(","))
            {
                int num = int.Parse(input[current.Start..current.End]);
                result.Add(num);
            }

            return BitwiseHelpers.EncodeAsInt64([.. result], BitsPerNumber);
        }

        private static long ParseButtons(ReadOnlySpan<char> input, int counters)
        {
            input = input[1..^1];
            int[] increases = new int[counters];

            foreach (Range current in input.Split(","))
            {
                int num = int.Parse(input[current.Start..current.End]);
                increases[num] = 1;
            }

            return BitwiseHelpers.EncodeAsInt64(increases, BitsPerNumber);
        }
    }
}
