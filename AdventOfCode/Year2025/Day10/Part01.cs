namespace AdventOfCode.Year2025.Day10;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int totalPresses = 0;

        foreach (string row in input)
        {
            Machine machine = new(row);
            int minimumPresses = machine.CountButtonPressesToTarget();
            totalPresses += minimumPresses;
        }

        return totalPresses.ToString();
    }

    private record Machine
    {
        public Machine(string input)
        {
            string[] components = input.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            this.Target = ParseTarget(components[0]);
            this.Buttons = [.. components[1..^1].Select(c => ParseButtons(c))];
        }

        public int Target { get; }

        public int[] Buttons { get; }

        public int CountButtonPressesToTarget()
        {
            int current = 0;
            List<int> seen = [];
            Queue<(int Current, int PressesSoFar)> states = [];
            states.Enqueue((current, 0));

            while (states.TryDequeue(out (int Current, int PressesSoFar) result))
            {
                if (result.Current == this.Target)
                {
                    return result.PressesSoFar;
                }

                if (seen.Contains(result.Current))
                {
                    continue;
                }

                seen.Add(result.Current);

                foreach (int button in this.Buttons)
                {
                    states.Enqueue((result.Current ^ button, result.PressesSoFar + 1));
                }
            }

            Assert.Fail("Couldn't find combination");
            return -1;
        }

        private static int ParseTarget(ReadOnlySpan<char> input)
        {
            input = input[1..^1];

            int target = 0;

            for (int index = 0; index < input.Length; index++)
            {
                if (input[index] == '#')
                {
                    target = target | (int)Math.Pow(2, index);
                }
            }

            return target;
        }

        private static int ParseButtons(ReadOnlySpan<char> input)
        {
            input = input[1..^1];
            int mask = 0;

            foreach (Range current in input.Split(","))
            {
                int num = int.Parse(input[current.Start..current.End]);
                mask = mask | (int)Math.Pow(2, num);
            }

            return mask;
        }
    }
}
