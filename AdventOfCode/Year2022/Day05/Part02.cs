namespace AdventOfCode.Year2022.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            (List<char>[] stacks, (int Count, int From, int To)[] instructions) = this.ParseInput(input);

            foreach ((int Count, int From, int To) instruction in instructions)
            {
                stacks[instruction.To].InsertRange(0, stacks[instruction.From].GetRange(0, instruction.Count));
                stacks[instruction.From].RemoveRange(0, instruction.Count);
            }

            return string.Concat(stacks.Select(x => x[0]));
        }

        private (List<char>[] stacks, (int Count, int From, int To)[]) ParseInput(string[] input)
        {
            int emptyLineIndex = Array.IndexOf(input, string.Empty);

            string[] rawConfiguration = input[0..emptyLineIndex];

            // Parse the initial configuration. First: how many stacks do we have?
            int stackCount = (rawConfiguration[0].Length + 1) / 4;

            var stacks = new List<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stacks[i] = new List<char>();
            }

            // Unlike Part 1, where we used a set of stacks and therefore had to process
            // the input from bottom to top, we're using lists this time. And to make the
            // subsequent code more readable, I'm going to pretend that the start of the
            // List is the "top" of the stack - so when we're moving crates we'll be
            // removing from the start of one list and inserting into the start of another.
            // It's probably horribly inefficient, but it'll be less of a headache to think
            // about.
            for (int row = 0; row < rawConfiguration.Length; row++)
            {
                for (int stack = 0; stack < stacks.Length; stack++)
                {
                    char crate = rawConfiguration[row][(stack * 4) + 1];
                    if (crate != ' ')
                    {
                        stacks[stack].Add(crate);
                    }
                }
            }

            (int, int, int)[] instructions = input[(emptyLineIndex + 1) ..]
                .Select(x => x.Split(' '))
                .Select(x => (int.Parse(x[1]), int.Parse(x[3]) - 1, int.Parse(x[5]) - 1))
                .ToArray();

            return (stacks, instructions);
        }
    }
}
