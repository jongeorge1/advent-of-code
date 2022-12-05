namespace AdventOfCode.Year2021.Day06
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var state = input.Split(',').Select(int.Parse).ToList();

            long totalLanternFish = state.Count;

            for (int i = 0; i < state.Count; i++)
            {
                totalLanternFish += Day06Common.CalculateDescendantCountAfterDays(80, state[i]);
            }

            return totalLanternFish.ToString();
        }
    }
}
