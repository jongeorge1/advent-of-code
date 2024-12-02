namespace AdventOfCode.Year2018.Day12
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var state = State.Parse(input[0].Substring(15));

            string[] ruleLines = new string[input.Length - 1];
            Array.Copy(input, 1, ruleLines, 0, ruleLines.Length);

            Rule[] rules = ruleLines.Select(Rule.Parse).ToArray();

            for (int i = 0; i < 20; i++)
            {
                state = state.Apply(rules);
            }

            return state.PotsContainingPlants.Sum().ToString();
        }
    }
}
