namespace AoC.Solutions.Year2015.Day07
{
    using System.Collections.Generic;

    public class DirectInputWire : IWire
    {
        private readonly int value;

        public DirectInputWire(string name, int value)
        {
            this.Name = name;
            this.value = value;
        }

        public string Name { get; }

        public int GetOutput(IDictionary<string, IWire> wires) => this.value;
    }
}
