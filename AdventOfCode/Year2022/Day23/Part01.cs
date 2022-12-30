namespace AdventOfCode.Year2022.Day23
{
    using AdventOfCode;

    public partial class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var map = new Map(input);

            for (int i = 0; i < 10; ++i)
            {
                map.ExecuteRound();
            }

            return map.CountEmptyTilesInOccupiedArea().ToString();
        }
    }
}