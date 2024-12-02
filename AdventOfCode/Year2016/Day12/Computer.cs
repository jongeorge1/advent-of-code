namespace AdventOfCode.Year2016.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Computer
    {
        private int location = 0;

        private string[][] program;

        private Dictionary<string, Action<string[]>> instructionMap;

        public Computer(string[] program)
        {
            this.program = program.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();

            this.instructionMap = new()
            {
                { "cpy", this.Cpy },
                { "inc", this.Inc },
                { "dec", this.Dec },
                { "jnz", this.Jnz },
            };
        }

        public Dictionary<string, int> Registers { get; } = new()
        {
            { "a", 0 },
            { "b", 0 },
            { "c", 0 },
            { "d", 0 },
        };

        public void Execute()
        {
            while (this.location < this.program.Length)
            {
                this.instructionMap[this.program[this.location][0]](this.program[this.location]);
            }
        }

        private void Cpy(string[] instruction)
        {
            string targetRegister = instruction[2];

            if (int.TryParse(instruction[1], out int value))
            {
                // It's a numeric value
                this.Registers[targetRegister] = value;
            }
            else
            {
                this.Registers[targetRegister] = this.Registers[instruction[1]];
            }

            ++this.location;
        }

        private void Inc(string[] instruction)
        {
            ++this.Registers[instruction[1]];
            ++this.location;
        }

        private void Dec(string[] instruction)
        {
            --this.Registers[instruction[1]];
            ++this.location;
        }

        private void Jnz(string[] instruction)
        {
            if (!int.TryParse(instruction[1], out int condition))
            {
                condition = this.Registers[instruction[1]];
            }

            if (!int.TryParse(instruction[2], out int offset))
            {
                offset = this.Registers[instruction[2]];
            }

            if (condition != 0)
            {
                this.location += offset;
            }
            else
            {
                ++this.location;
            }
        }
    }
}
