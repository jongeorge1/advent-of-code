namespace AdventOfCode.Year2021.Day06
{
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var state = input[0].Split(',').Select(int.Parse).ToList();

            long totalLanternFish = state.Count;

            for (int i = 0; i < state.Count; i++)
            {
                totalLanternFish += Day06Common.CalculateDescendantCountAfterDays(256, state[i]);
            }

            return totalLanternFish.ToString();
        }
    }
}
