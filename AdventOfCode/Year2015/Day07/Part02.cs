namespace AdventOfCode.Year2015.Day07
{
    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var circuit = new Circuit(input);

            // Override b
            circuit.Wires["b"] = new DirectInputWire("b", 956);

            IWire targetWire = circuit.Wires["a"];
            return targetWire.GetOutput(circuit.Wires).ToString();
        }
    }
}
