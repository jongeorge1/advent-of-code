namespace AdventOfCode.Year2022.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Device
    {
        private Dictionary<string, Action<string[]>> instructionMap;
        private string[][] program;
        private int cycle;

        public Device(string[] program)
        {
            this.program = program.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            this.instructionMap = new()
            {
                { "addx", this.AddX },
                { "noop", this.NoOp },
            };
            this.X = 1;
            this.cycle = 0;
        }

        public event EventHandler<TickEventArgs>? Tick;

        public int X
        {
            get;
            private set;
        }

        public void Execute()
        {
            this.OnTick();

            foreach (string[] instruction in this.program)
            {
                this.instructionMap[instruction[0]](instruction);
            }
        }

        private void AddX(string[] instruction)
        {
            this.OnTick();

            this.X += int.Parse(instruction[1]);

            this.OnTick();
        }

        private void NoOp(string[] instruction)
        {
            this.OnTick();
        }

        private void OnTick()
        {
            this.Tick?.Invoke(
                this,
                TickEventArgs.Create(++this.cycle));
        }
    }
}
