namespace AdventOfCode.Year2022.Day12
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var algorithm = new HillClimbingAlgorithm(input);

            algorithm.TryFindShortestPathBetween(algorithm.Start, algorithm.End, out int? steps);
            return steps!.Value.ToString();
        }
    }
}
