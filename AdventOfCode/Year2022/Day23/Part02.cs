namespace AdventOfCode.Year2022.Day23
{
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var map = new Map(input);
            int roundCount = 0;

            bool atLeastOneElfMoved;

            do
            {
                atLeastOneElfMoved = map.ExecuteRound();
                ++roundCount;
            }
            while (atLeastOneElfMoved);

            return roundCount.ToString();
        }
    }
}
