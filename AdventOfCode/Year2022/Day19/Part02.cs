namespace AdventOfCode.Year2022.Day19
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Span<Blueprint> blueprints = stackalloc Blueprint[3];
            int processedBlueprintCount = 0;

            StringExtensions.StringSplitEnumerator enumerator = input.SplitLines();

            while (processedBlueprintCount < 3)
            {
                enumerator.MoveNext();
                blueprints[processedBlueprintCount++] = Blueprint.FromInputString(enumerator.Current.Line);
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
