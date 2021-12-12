namespace AoC.Solutions.Year2016.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Computer
    {
        private int location = 0;

        private string[][] program;

        private Dictionary<string, Action<string[]>> instructionMap;

        public Computer(string program)
        {
            this.program = program.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();

            this.instructionMap = new ()
            {
                { "cpy", this.Cpy },
                { "inc", this.Inc },
                { "dec", this.Dec },
                { "jnz", this.Jnz },
                { "tgl", this.Tgl },
            };
        }

        public Dictionary<string, int> Registers { get; } = new ()
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

        private void Tgl(string[] instruction)
        {
        }

        private void Cpy(string[] instruction)
        {
            string targetRegister = instruction[2];

            this.Registers[targetRegister] = this.GetValueOrRegister(instruction[1]);

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
            int condition = this.GetValueOrRegister(instruction[1]);
            int offset = this.GetValueOrRegister(instruction[2]);

            if (condition != 0)
            {
                this.location += offset;
            }
            else
            {
                ++this.location;
            }
        }

        private int GetValueOrRegister(string instruction)
        {
            if (!int.TryParse(instruction, out int result))
            {
                result = this.Registers[instruction];
            }

            return result;
        }
    }
}
