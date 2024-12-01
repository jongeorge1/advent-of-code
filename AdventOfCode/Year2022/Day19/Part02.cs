namespace AdventOfCode.Year2022.Day19
{
    using System;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Span<Blueprint> blueprints = stackalloc Blueprint[3];
            int processedBlueprintCount = 0;

            int index = 0;

            while (processedBlueprintCount < 3)
            {
                blueprints[processedBlueprintCount++] = Blueprint.FromInputString(input[index++]);
            }

            int sum = 1;
            for (int i = 0; i < processedBlueprintCount; ++i)
            {
                sum *= BlueprintQualityAssesor.DetermineLargestNumberOfGeodesThatCanBeOpened(ref blueprints[i], 32);
            }

            return sum.ToString();
        }
    }
}
