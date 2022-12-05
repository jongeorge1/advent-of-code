namespace AdventOfCode.Year2022.Day03
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(Environment.NewLine)
                .Select(line => (line[..(line.Length / 2)], line[(line.Length / 2)..]))
                .Select(rucksack => rucksack.Item1.First(rucksack.Item2.Contains))
                .Sum(b => b <= 'Z' ? b - 38 : b - 96)
                .ToString();
        }
    }
}
