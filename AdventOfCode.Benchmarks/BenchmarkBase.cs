namespace AdventOfCode.Benchmarks;

using BenchmarkDotNet.Attributes;

public class BenchmarkBase
{
    private readonly string[] data;

    private readonly ISolution part1Subject;

    private readonly ISolution part2Subject;

    public BenchmarkBase(int year, int day)
    {
        string path = Path.Combine($"Year{year:D2}", $"Day{day:D2}", "input.txt");
        data = File.ReadAllLines(path);
        this.part1Subject = SolutionFactory.GetSolution(year, day, 1);
        this.part2Subject = SolutionFactory.GetSolution(year, day, 2);
    }

    [Benchmark]
    public void Part1()
    {
        this.part1Subject.Solve(this.data);
    }


    [Benchmark]
    public void Part2()
    {
        this.part2Subject.Solve(this.data);
    }
}
