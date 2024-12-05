namespace AdventOfCode.Year2018.Day21
{
    using System.Collections.Generic;
    using System.Linq;
    using Instruction = AdventOfCode.Year2018.Day19.Instruction;
    using Operation = AdventOfCode.Year2018.Day19.Operation;
    using Parser = AdventOfCode.Year2018.Day19.Parser;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int instructionPointerRegister = int.Parse(input[0].Split(' ')[1]);
            int instructionPointer = 0;

            Instruction[] instructions = input
                .Skip(1)
                .Select(Parser.BuildInstructionFromInputLine)
                .ToArray();

            var operations = Operation.All.ToDictionary(x => x.Name, x => x.Execute);

            int[] registers = [0, 0, 0, 0, 0, 0];
            var seenR2Values = new List<int>(1000000);

            // We can assume from the question that r2 is going to change, and at some point we're going to
            // hit a loop. Therefore we need to watch the comparisons, store the values from r2, and the
            // first time we see a number crop up twice, we know the answer is the previous value we stored.
            while (instructionPointer >= 0 && instructionPointer < instructions.Length)
            {
                ////Console.Write($"{instructionPointer}:{registers[0]}:{registers[1]}:{registers[2]}:{registers[3]}:{registers[4]}:{registers[5]:D4}:");

                if (instructionPointer == 30)
                {
                    if (seenR2Values.Contains(registers[2]))
                    {
                        return seenR2Values.Last().ToString();
                    }

                    seenR2Values.Add(registers[2]);
                }

                registers[instructionPointerRegister] = instructionPointer;

                Instruction targetInstruction = instructions[instructionPointer];
                ////Console.Write($"{targetInstruction.OpName}:{targetInstruction.A}:{targetInstruction.B}:{targetInstruction.C}:");

                operations[targetInstruction.OpName](registers, targetInstruction);

                instructionPointer = registers[instructionPointerRegister];
                instructionPointer++;
                ////Console.WriteLine($"{registers[0]:D4}:{registers[1]:D4}:{registers[2]:D4}:{registers[3]:D4}:{registers[4]:D4}:{registers[5]:D4}");
            }

            return string.Empty;
        }
    }
}
