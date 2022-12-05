namespace AdventOfCode.Year2015.Day07
{
    using System.Collections.Generic;

    public class AndGateInputWire : IWire
    {
        private readonly string input1;
        private readonly string input2;
        private int? signal;

        public AndGateInputWire(string name, string input1, string input2)
        {
            this.Name = name;
            this.input1 = input1;
            this.input2 = input2;
        }

        public string Name { get; }

        public int GetOutput(IDictionary<string, IWire> wires)
        {
            if (!this.signal.HasValue)
            {
                int val1 = int.TryParse(this.input1, out int numericInput1) ? numericInput1 : wires[this.input1].GetOutput(wires);
                int val2 = int.TryParse(this.input2, out int numericInput2) ? numericInput2 : wires[this.input2].GetOutput(wires);

                this.signal = val1 & val2;
            }

            return this.signal.Value;
        }
    }
}
