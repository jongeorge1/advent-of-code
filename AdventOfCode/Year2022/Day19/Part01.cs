namespace AdventOfCode.Year2022.Day19
{
    using System;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Span<Blueprint> blueprints = stackalloc Blueprint[30];
            int processedBlueprintCount = 0;

            foreach (string current in input)
            {
                blueprints[processedBlueprintCount++] = Blueprint.FromInputString(current);
            }

            blueprints = blueprints[0..processedBlueprintCount];

            int sum = 0;
            for (int i = 0; i < processedBlueprintCount; ++i)
            {
                sum += BlueprintQualityAssesor.CalculateQualityLevel(ref blueprints[i], 24);
            }

            return sum.ToString();
        }
    }
}
