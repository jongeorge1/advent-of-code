namespace AdventOfCode.Year2016.Day11
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var area = new Area(input);

            return Day11Common.GetStepsToBringAllObjectsToFourthFloor(area).ToString();
        }
    }
}
