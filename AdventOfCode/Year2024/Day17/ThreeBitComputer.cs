namespace AdventOfCode.Year2024.Day17;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Year2015.Day07;

public class ThreeBitComputer
{
    private readonly long[] registers = [0, 0, 0];
    private readonly List<int> output = [];

    private readonly Func<int, int>[] operations;

    public ThreeBitComputer(string[] input)
    {
        this.registers[0] = long.Parse(input[0][12..]);
        this.registers[1] = long.Parse(input[1][12..]);
        this.registers[2] = long.Parse(input[2][12..]);

        this.RawProgram = input[4][9..];
        this.Program = this.RawProgram.Split(',').Select(int.Parse).ToArray();

        this.operations = [this.Adv, this.Bxl, this.Bst, this.Jnz, this.Bxc, this.Out, this.Bdv, this.Cdv];
    }

    public int[] Program { get; }

    public string RawProgram { get; }

    public int[] Execute()
    {
        int instructionPointer = 0;

        while (instructionPointer < this.Program.Length)
        {
            int opCode = this.Program[instructionPointer];
            instructionPointer = this.operations[opCode](instructionPointer);
        }

        return [..this.output];
    }

    public void Reset()
    {
        this.registers[0] = 0;
        this.registers[1] = 0;
        this.registers[2] = 0;
        this.output.Clear();
    }

    public void SetRegister(char register, long value)
    {
        this.registers[register - 'A'] = value;
    }

    private int Adv(int instructionPointer)
    {
        long numerator = this.registers[0];
        long operandValue = this.GetComboOperandValue(this.Program[instructionPointer + 1]);
        string operandDescription = this.GetComboOperandDescription(this.Program[instructionPointer + 1]);
        long denominator = (int)Math.Pow(2, operandValue);
        long result = numerator / denominator;
        this.registers[0] = result;

        ////Console.WriteLine($"{instructionPointer}: ADV {operandDescription}: (A = {numerator} / 2^{operandValue} = {numerator} / {denominator} = {result})");

        return instructionPointer + 2;
    }

    private int Bxl(int instructionPointer)
    {
        long registerValue = this.registers[1];
        long operandValue = this.Program[instructionPointer + 1];

        long result = registerValue ^ operandValue;
        this.registers[1] = result;

        ////Console.WriteLine($"{instructionPointer}: BXL {operandValue}: (B = B XOR {operandValue} = {registerValue} XOR {operandValue} = {result})");

        return instructionPointer + 2;
    }

    private int Bst(int instructionPointer)
    {
        long operandValue = this.GetComboOperandValue(this.Program[instructionPointer + 1]);
        string operandDescription = this.GetComboOperandDescription(this.Program[instructionPointer + 1]);
        long result = operandValue % 8;
        this.registers[1] = result;

        ////Console.WriteLine($"{instructionPointer}: BST {operandDescription}: (B = {operandValue} % 8 = {result})");

        return instructionPointer + 2;
    }

    private int Jnz(int instructionPointer)
    {
        long registerValue = this.registers[0];

        if (registerValue == 0)
        {
            ////Console.WriteLine($"{instructionPointer}: JNZ if A = 0: (A = {registerValue} = NO JUMP)");

            return instructionPointer + 2;
        }

        int operandValue = this.Program[instructionPointer + 1];

        ////Console.WriteLine($"{instructionPointer}: JNZ if A=0: (A = {registerValue} = JNZ {operandValue})");

        return operandValue;
    }

    private int Bxc(int instructionPointer)
    {
        long registerBValue = this.registers[1];
        long registerCValue = this.registers[2];
        long result = registerBValue ^ registerCValue;

        ////Console.WriteLine($"{instructionPointer}: BXC: (B = B XOR C = {registerBValue} XOR {registerCValue} = {result})");

        this.registers[1] = result;

        return instructionPointer + 2;
    }

    private int Out(int instructionPointer)
    {
        long operandValue = this.GetComboOperandValue(this.Program[instructionPointer + 1]);
        string operandDescription = this.GetComboOperandDescription(this.Program[instructionPointer + 1]);
        int outputValue = (int)(operandValue % 8);
        this.output.Add(outputValue);

        ////Console.WriteLine($"{instructionPointer}: OUT {operandDescription} % 8: ({operandValue} % 8 = {outputValue})");

        return instructionPointer + 2;
    }

    private int Bdv(int instructionPointer)
    {
        long numerator = this.registers[0];
        long operandValue = this.GetComboOperandValue(this.Program[instructionPointer + 1]);
        string operandDescription = this.GetComboOperandDescription(this.Program[instructionPointer + 1]);
        long denominator = (int)Math.Pow(2, operandValue);
        long result = numerator / denominator;
        this.registers[1] = result;

        ////Console.WriteLine($"{instructionPointer}: BDV {operandDescription}: (B = {numerator} / 2^{operandValue} = {numerator} / {denominator} = {result})");

        return instructionPointer + 2;
    }

    private int Cdv(int instructionPointer)
    {
        long numerator = this.registers[0];
        long operandValue = this.GetComboOperandValue(this.Program[instructionPointer + 1]);
        string operandDescription = this.GetComboOperandDescription(this.Program[instructionPointer + 1]);
        long denominator = (int)Math.Pow(2, operandValue);
        long result = numerator / denominator;
        this.registers[2] = result;

        ////Console.WriteLine($"{instructionPointer}: CDV {operandDescription}: (C = {numerator} / 2^{operandValue} = {numerator} / {denominator} = {result})");

        return instructionPointer + 2;
    }

    private long GetComboOperandValue(int operand)
    {
        return operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => this.registers[0],
            5 => this.registers[1],
            6 => this.registers[2],
            _ => throw new InvalidOperationException($"Invalid operand {operand}"),
        };
    }

    private string GetComboOperandDescription(int operand)
    {
        return operand switch
        {
            0 => "0",
            1 => "1",
            2 => "2",
            3 => "3",
            4 => $"A ({this.registers[0]})",
            5 => $"B ({this.registers[1]})",
            6 => $"C ({this.registers[2]})",
            _ => throw new InvalidOperationException($"Invalid operand {operand}"),
        };
    }
}
