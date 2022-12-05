namespace AoC.Solutions.Year2022.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(new[] { Environment.NewLine, "-", "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Chunk(4)
                .Count(assignment =>
                    (assignment[0] >= assignment[2] && assignment[1] <= assignment[3]) ||
                    (assignment[2] >= assignment[0] && assignment[3] <= assignment[1]))
                .ToString();
        }
    }
}
