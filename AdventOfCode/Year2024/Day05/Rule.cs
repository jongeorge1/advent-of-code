namespace AdventOfCode.Year2024.Day05;

public readonly record struct Rule
{
    public Rule(string rule)
    {
        string[] segments = rule.Split("|");
        this.First = int.Parse(segments[0]);
        this.Second = int.Parse(segments[1]);
    }

    public readonly int First { get; }

    public readonly int Second { get; }
}
