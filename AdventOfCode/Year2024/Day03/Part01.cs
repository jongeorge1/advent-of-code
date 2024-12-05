namespace AdventOfCode.Year2024.Day03;

using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode;

public partial class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        string allInstructions = string.Join(string.Empty, input);
        MatchCollection matches = InstructionRegex().Matches(allInstructions);
        long result = matches.Aggregate(0L, (acc, match) => acc + (long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value)));
        return result.ToString();
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex InstructionRegex();
}
