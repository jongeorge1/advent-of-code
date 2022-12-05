namespace AdventOfCode.Year2015.Day07
{
    using System.Collections.Generic;

    public class LeftShiftGateInputWire : IWire
    {
        private readonly string input1;
        private readonly int input2;
        private int? signal;

        public LeftShiftGateInputWire(string name, string input1, int input2)
        {
            this.Name = name;
            this.input1 = input1;
            this.input2 = input2;
        }

        public string Name { get; }

        public int GetOutput(IDictionary<string, IWire> wires)
        {
            return this.signal ??= wires[this.input1].GetOutput(wires) << this.input2;
        }
    }
}
