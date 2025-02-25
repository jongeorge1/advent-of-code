﻿namespace AdventOfCode.Year2019.Day02
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] program = input[0]
                .Split(",")
                .Select(x => int.Parse(x))
                .ToArray();

            if (program.Length > 15)
            {
                program[1] = 12;
                program[2] = 2;
            }

            int pointer = 0;

            while (program[pointer] != 99)
            {
                if (program[pointer] == 1)
                {
                    program[program[pointer + 3]] = program[program[pointer + 1]] + program[program[pointer + 2]];
                }
                else if (program[pointer] == 2)
                {
                    program[program[pointer + 3]] = program[program[pointer + 1]] * program[program[pointer + 2]];
                }

                pointer += 4;
            }

            return program[0].ToString();
        }
    }
}
