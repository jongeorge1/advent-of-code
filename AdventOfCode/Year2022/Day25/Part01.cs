namespace AdventOfCode.Year2022.Day25
{
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            long total = 0;

            foreach (string current in input)
            {
                total += SnafuConverter.ToLong(current);
            }

            return SnafuConverter.ToSnafu(total).ToString();
        }
    }
}
