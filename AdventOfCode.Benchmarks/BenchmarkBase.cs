namespace AdventOfCode.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    public class BenchmarkBase
    {
        private readonly string[] data;

        private readonly ISolution subject;

        public BenchmarkBase(int year, int day, int part)
        {
            string path = Path.Combine($"Year{year:D2}", $"Day{day:D2}", "input.txt");
            data = File.ReadAllLines(path);
            this.subject = SolutionFactory.GetSolution(year, day, part);
        }

        [Benchmark]
        public void RunAgainstRealInput()
        {
            subject.Solve(this.data);
        }
    }
}
