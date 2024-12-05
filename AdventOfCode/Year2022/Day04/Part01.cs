namespace AdventOfCode.Year2022.Day04
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return input
                .Select(int.Parse)
                .Chunk(4)
                .Count(assignment =>
                    (assignment[0] >= assignment[2] && assignment[1] <= assignment[3]) ||
                    (assignment[2] >= assignment[0] && assignment[3] <= assignment[1]))
                .ToString();
        }
    }
}
