namespace AdventOfCode.Year2024.Day05;

using System;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int separatorIndex = Array.IndexOf(input, string.Empty);

        Rule[] rules = input[..separatorIndex].Select(x => new Rule(x)).ToArray();
        Update[] updates = input[(separatorIndex + 1) ..].Select(x => new Update(x)).ToArray();

        return updates.Where(x => x.IsValid(rules)).Sum(x => x.MiddlePageNumber).ToString();
    }
}
