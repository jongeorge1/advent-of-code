namespace AdventOfCode.Year2022.Day12
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var algorithm = new HillClimbingAlgorithm(input);

            int minimumSteps = int.MaxValue;

            foreach (KeyValuePair<(int X, int Y), char> location in algorithm.Map.Where(x => x.Value == 'a'))
            {
                if (algorithm.TryFindShortestPathBetween(location.Key, algorithm.End, out int? steps) && steps < minimumSteps)
                {
                    minimumSteps = steps.Value;
                }
            }

            return minimumSteps.ToString();
        }
    }
}
