namespace AdventOfCode.Year2015.Day07
{
    using System.Collections.Generic;

    public interface IWire
    {
        public string Name { get; }

        int GetOutput(IDictionary<string, IWire> wires);
    }
}
