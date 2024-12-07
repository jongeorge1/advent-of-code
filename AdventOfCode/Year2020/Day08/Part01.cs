namespace AdventOfCode.Year2020.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Instruction[] data = input
                .Select(x => new Instruction(x))
                .ToArray();

            var state = new ConsoleState
            {
                Accumulator = 0,
                Pointer = 0,
                Instructions = data,
                ExecutedInstructions = new List<int>(),
            };

            state.RunToEnd();

            return state.Accumulator.ToString();
        }
    }
}
