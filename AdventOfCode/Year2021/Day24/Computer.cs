namespace AdventOfCode.Year2021.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Computer
    {
        private int location = 0;

        private string[][] program;

        private Dictionary<string, Action<string[]>> instructionMap;

        private bool verbose;

        private int[] inputs;

        private int nextInput = 0;

        public Computer(string[] program, string modelNumber, bool verbose = false)
        {
            this.program = program.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            this.inputs = modelNumber.ToCharArray().Select(x => x - '1' + 1).ToArray();
            this.verbose = verbose;
            this.instructionMap = new ()
            {
                { "inp", this.Inp },
                { "add", this.Add },
                { "mul", this.Mul },
                { "div", this.Div },
                { "mod", this.Mod },
                { "eql", this.Eql },
            };
        }

        public Dictionary<string, long> Registers { get; } = new ()
        {
            { "w", 0 },
            { "x", 0 },
            { "y", 0 },
            { "z", 0 },
        };

        public void Execute()
        {
            while (this.location < this.program.Length)
            {
                this.instructionMap[this.program[this.location][0]](this.program[this.location]);
            }
        }

        private void NoOp(string[] instruction)
        {
            throw new NotImplementedException();
        }

        private void Inp(string[] instruction)
        {
            this.Registers[instruction[1]] = this.inputs[this.nextInput];
            ++this.nextInput;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing input value '{this.Registers[instruction[1]]}' in register {instruction[1]}. {this.GetRegistersAsString()}");
            }
        }

        private void Add(string[] instruction)
        {
            long result = this.Registers[instruction[1]] + this.GetValueOrRegister(instruction[2]);
            this.Registers[instruction[1]] = result;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing '{instruction[1]}' + '{instruction[2]}' = {result} in '{instruction[1]}'. {this.GetRegistersAsString()}");
            }
        }

        private void Mul(string[] instruction)
        {
            long result = this.Registers[instruction[1]] * this.GetValueOrRegister(instruction[2]);
            this.Registers[instruction[1]] = result;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing '{instruction[1]}' * '{instruction[2]}' = {result} in '{instruction[1]}'. {this.GetRegistersAsString()}");
            }
        }

        private void Div(string[] instruction)
        {
            long result = (long)Math.Floor((decimal)this.Registers[instruction[1]] / this.GetValueOrRegister(instruction[2]));
            this.Registers[instruction[1]] = result;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing '{instruction[1]}' / '{instruction[2]}' = {result} in '{instruction[1]}'. {this.GetRegistersAsString()}");
            }
        }

        private void Mod(string[] instruction)
        {
            long result = this.Registers[instruction[1]] % this.GetValueOrRegister(instruction[2]);
            this.Registers[instruction[1]] = result;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing '{instruction[1]}' % '{instruction[2]}' = {result} in '{instruction[1]}'. {this.GetRegistersAsString()}");
            }
        }

        private void Eql(string[] instruction)
        {
            int result = this.Registers[instruction[1]] == this.GetValueOrRegister(instruction[2]) ? 1 : 0;
            this.Registers[instruction[1]] = result;
            ++this.location;

            if (this.verbose)
            {
                Console.WriteLine($"${this.location}: Storing '{instruction[1]}' == '{instruction[2]}' = {result} in '{instruction[1]}'. {this.GetRegistersAsString()}");
            }
        }

        private long GetValueOrRegister(string instruction)
        {
            if (!long.TryParse(instruction, out long result))
            {
                result = this.Registers[instruction];
            }

            return result;
        }

        private string GetRegistersAsString()
        {
            return string.Join(", ", this.Registers.Select(x => $"{x.Key} = {x.Value}"));
        }
    }
}
