namespace AdventOfCode.Year2024.Day17;

using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        ThreeBitComputer computer = new(input);
        int[] result = computer.Execute();

        return string.Join(",", result);
    }
}
