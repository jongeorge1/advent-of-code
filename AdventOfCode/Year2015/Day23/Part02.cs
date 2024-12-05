namespace AdventOfCode.Year2015.Day23
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            List<(string Instruction, string Parameters)> instructions = input.Select(x => (x[..3], x[4..])).ToList();

            var registers = new Dictionary<string, int>()
            {
                { "a", 1 },
                { "b", 0 },
            };

            int pointer = 0;

            while (pointer < instructions.Count)
            {
                (string instruction, string parameters) = instructions[pointer];
                switch (instruction)
                {
                    case "hlf":
                        registers[parameters] /= 2;
                        ++pointer;
                        break;

                    case "tpl":
                        registers[parameters] *= 3;
                        ++pointer;
                        break;

                    case "inc":
                        ++registers[parameters];
                        ++pointer;
                        break;

                    case "jmp":
                        pointer += int.Parse(parameters);
                        break;

                    case "jie":
                        if (registers[parameters[..1]] % 2 == 0)
                        {
                            pointer += int.Parse(parameters[3..]);
                        }
                        else
                        {
                            ++pointer;
                        }

                        break;

                    case "jio":
                        if (registers[parameters[..1]] == 1)
                        {
                            pointer += int.Parse(parameters[3..]);
                        }
                        else
                        {
                            ++pointer;
                        }

                        break;
                }
            }

            return registers["b"].ToString();
        }
    }
}