namespace AoC.Solutions.Year2021.Day08
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            // Segment counts:
            // 1 - 2
            // 2 - 5
            // 3 - 5
            // 4 - 4
            // 5 - 5
            // 6 - 6
            // 7 - 3
            // 8 - 7
            // 9 - 6
            // This means the numbers that use unique segment counts are:
            // 1, 4, 7 and 8
            var inputLines = input.Split(Environment.NewLine).ToList();

            (string[] Inputs, string[] Outputs)[] digits = inputLines.Select(x => x.Split('|'))
                .Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .ToArray();

            // Now we have to count up the number of outputs with the specified numbers of digits
            int target = digits.SelectMany(x => x.Outputs.Select(o => o.Length)).Count(l => l == 2 || l == 4 || l == 3 || l == 7);

            return target.ToString();
        }
    }
}
