namespace AdventOfCode.Year2016.Day11
{
    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var area = new Area(input);
            area.Floors[0].AddChips(new[] { "elerium", "dilithium" });
            area.Floors[0].AddGenerators(new[] { "elerium", "dilithium" });

            return Day11Common.GetStepsToBringAllObjectsToFourthFloor(area).ToString();
        }
    }
}
