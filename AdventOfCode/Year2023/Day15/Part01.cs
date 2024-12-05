namespace AdventOfCode.Year2023.Day15
{
    using AdventOfCode;
    using AdventOfCode.Helpers;
    using static AdventOfCode.Helpers.StringExtensions;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            // Only one line in the input, which is a comma separated string
            StringSplitEnumerator enumerator = input[0].OptimizedSplit(",");
            while (enumerator.MoveNext())
            {
                runningTotal += Hash(enumerator.Current);
            }

            return runningTotal.ToString();
        }

        private static int Hash(StringSplitEntry current)
        {
            int runningTotal = 0;

            for (int i = 0; i < current.Line.Length; ++i)
            {
                runningTotal += (int)current.Line[i];
                runningTotal *= 17;
                runningTotal %= 256;
            }

            return runningTotal;
        }
    }
}
