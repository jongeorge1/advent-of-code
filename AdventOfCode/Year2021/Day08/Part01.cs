namespace AdventOfCode.Year2021.Day08
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var inputLines = input.Split(Environment.NewLine).ToList();

            (string[] Inputs, string[] Outputs)[] digits = inputLines.Select(x => x.Split('|'))
                .Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .ToArray();

            int target = digits.SelectMany(x => x.Outputs.Select(o => o.Length)).Count(l => l == 2 || l == 4 || l == 3 || l == 7);

            return target.ToString();
        }
    }
}
