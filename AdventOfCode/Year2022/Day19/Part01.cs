namespace AdventOfCode.Year2022.Day19
{
    using System;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Span<Blueprint> blueprints = stackalloc Blueprint[30];
            int processedBlueprintCount = 0;

            foreach (StringExtensions.StringSplitEntry current in input.SplitLines())
            {
                blueprints[processedBlueprintCount++] = Blueprint.FromInputString(current.Line);
            }

            blueprints = blueprints[0..processedBlueprintCount];

            return string.Empty;
        }
    }
}
