namespace AdventOfCode.Year2018.Day19
{
    using System;
    using System.Linq;

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

            while (instructionPointer >= 0 && instructionPointer < instructions.Length)
            {
                ////Console.Write($"{instructionPointer}:{registers[0]}:{registers[1]}:{registers[2]}:{registers[3]}:{registers[4]}:{registers[5]:D4}:");

                registers[instructionPointerRegister] = instructionPointer;

                Instruction targetInstruction = instructions[instructionPointer];
                ////Console.Write($"{targetInstruction.OpName}:{targetInstruction.A}:{targetInstruction.B}:{targetInstruction.C}:");

                operations[targetInstruction.OpName](registers, targetInstruction);

                instructionPointer = registers[instructionPointerRegister];
                instructionPointer++;

                ////Console.WriteLine($"{registers[0]:D4}:{registers[1]:D4}:{registers[2]:D4}:{registers[3]:D4},{registers[4]:D4},{registers[5]:D4}");
            }

            return registers[0].ToString();
        }
    }
}
