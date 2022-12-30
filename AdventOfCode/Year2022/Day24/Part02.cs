namespace AdventOfCode.Year2022.Day24
{
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var map = new BlizzardBasin(input);
            var totalTime = map.FindTimeFromEntranceToExit(0);
            totalTime = map.FindTimeFromExitToEntrance(totalTime);
            totalTime = map.FindTimeFromEntranceToExit(totalTime);

            return totalTime.ToString();
        }
    }
}
