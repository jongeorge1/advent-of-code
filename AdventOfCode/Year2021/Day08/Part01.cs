namespace AdventOfCode.Year2021.Day08
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            (string[] Inputs, string[] Outputs)[] digits = input.Select(x => x.Split('|'))
                .Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .ToArray();

            int target = digits.SelectMany(x => x.Outputs.Select(o => o.Length)).Count(l => l == 2 || l == 4 || l == 3 || l == 7);

            return target.ToString();
        }
    }
}
