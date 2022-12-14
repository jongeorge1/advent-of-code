namespace AdventOfCode.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [MemoryDiagnoser]
    public class Year2022Day01Part01 : BenchmarkBase
    {
        public Year2022Day01Part01() : base(2022, 1, 1)
        {
        }
    }
}
