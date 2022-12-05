namespace AdventOfCode.Year2022.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (Stack<char>[] stacks, (int Count, int From, int To)[] instructions) = this.ParseInput(input);

            foreach ((int Count, int From, int To) instruction in instructions)
            {
                for (int i = 0; i < instruction.Count; i++)
                {
                    stacks[instruction.To].Push(stacks[instruction.From].Pop());
                }
            }

            return string.Concat(stacks.Select(x => x.Peek()));
        }

        private (Stack<char>[] stacks, (int Count, int From, int To)[]) ParseInput(string input)
        {
            string[] sections = input.Split(Environment.NewLine + Environment.NewLine);

            string[] rawConfiguration = sections[0].Split(Environment.NewLine);

            // Parse the initial configuration. First: how many stacks do we have?
            int stackCount = (rawConfiguration[0].Length + 1) / 4;

            var stacks = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stacks[i] = new Stack<char>();
            }

            // To "load up" the stacks with their initial state, we need to process the input from
            // bottom to top (ignoring the final line, because it just contains stack numbers).
            for (int row = rawConfiguration.Length - 2; row >= 0; row--)
            {
                for (int stack = 0; stack < stacks.Length; stack++)
                {
                    char crate = rawConfiguration[row][(stack * 4) + 1];
                    if (crate != ' ')
                    {
                        stacks[stack].Push(crate);
                    }
                }
            }

            // When we process the instructions, we will subtract 1 from each of the stack numbers so they
            // can be used directly as indexes into our array of stacks.
            (int, int, int)[] instructions = sections[1].Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None)
                .Chunk(6)
                .Select(x => (int.Parse(x[1]), int.Parse(x[3]) - 1, int.Parse(x[5]) - 1))
                .ToArray();

            return (stacks, instructions);
        }
    }
}
