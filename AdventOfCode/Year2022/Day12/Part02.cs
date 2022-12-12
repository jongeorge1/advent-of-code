namespace AdventOfCode.Year2022.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var algorithm = new HillClimbingAlgorithm(input);

            int minimumSteps = int.MaxValue;

            // I wasn't expecting this parallelisation to work; I thought there was too much opportunity for a race condition
            // between the comparison of current steps to minimum steps and the update of minimum steps. However, I've run it
            // a lot of times and it's given the same answer each time.
            Parallel.ForEach(
                algorithm.Map.Where(x => x.Value == 'a'),
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount / 2 },
                location =>
            {
                if (algorithm.TryFindShortestPathBetween(location.Key, algorithm.End, out int? steps) && steps < minimumSteps)
                {
                    minimumSteps = steps.Value;
                }
            });

            return minimumSteps.ToString();
        }
    }
}
