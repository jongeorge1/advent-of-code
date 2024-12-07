namespace AdventOfCode.Year2018.Day16
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            (Sample[] samples, Instruction[] instructions) = Parser.Parse(input[0]);

            IEnumerable<(Sample x, int)> testResults = samples.Select(x => (x, x.GetMatchingOperationsCount()));

            int result = testResults.Count(x => x.Item2 >= 3);

            return result.ToString();
        }
    }
}
