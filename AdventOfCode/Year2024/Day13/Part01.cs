namespace AdventOfCode.Year2024.Day13;

using System;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        ClawMachine[] clawMachines = ClawMachine.FromInput(input);

        return clawMachines.Sum(x => x.GetCostOfCheapestSolution()).ToString();
    }
}
