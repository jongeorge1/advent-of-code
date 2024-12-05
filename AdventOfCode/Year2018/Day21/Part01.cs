namespace AdventOfCode.Year2018.Day21
{
    using System.Linq;
    using Instruction = AdventOfCode.Year2018.Day19.Instruction;
    using Operation = AdventOfCode.Year2018.Day19.Operation;
    using Parser = AdventOfCode.Year2018.Day19.Parser;

    public class Part01 : ISolution
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

            // From looking at the program, r0 is only used for one check - in line 30, and if it's equal
            // at that point to the value in r2 then the program will exit. So we need to know what the
            // value of r2 is the first time we execute line 30
            while (instructionPointer >= 0 && instructionPointer < instructions.Length)
            {
                ////Console.Write($"{instructionPointer}:{registers[0]}:{registers[1]}:{registers[2]}:{registers[3]}:{registers[4]}:{registers[5]:D4}:");

                if (instructionPointer == 30)
                {
                    return registers[2].ToString();
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
