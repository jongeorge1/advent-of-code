namespace AdventOfCode.Year2024.Day07;

using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        CalibrationEquation[] equations = input.Select(x => new CalibrationEquation(x)).ToArray();
        return equations.Where(x => x.IsValidForPart1()).Sum(x => x.TestValue).ToString();
    }
}
