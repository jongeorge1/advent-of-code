namespace AdventOfCode.Year2024.Day13;

using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        ClawMachine[] clawMachines = ClawMachine.FromInput(input, 10000000000000);

        return clawMachines.Sum(x => x.GetCostOfCheapestSolution()).ToString();
    }
}
