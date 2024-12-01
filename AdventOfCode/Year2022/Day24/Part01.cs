namespace AdventOfCode.Year2022.Day24
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var map = new BlizzardBasin(input);
            var result = map.FindTimeFromEntranceToExit(0);
            return result.ToString();
        }
    }
}
