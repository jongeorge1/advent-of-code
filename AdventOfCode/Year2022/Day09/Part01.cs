namespace AdventOfCode.Year2022.Day09
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return KnotSimulation.Run(input, 2);
        }
    }
}
