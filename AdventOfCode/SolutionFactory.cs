namespace AdventOfCode
{
    using System;

    public static class SolutionFactory
    {
        public static ISolution GetSolution(int year, int day, int part)
        {
            string className = $"AdventOfCode.Year{year:D4}.Day{day:D2}.Part{part:D2}";
            Type? targetType = typeof(ISolution).Assembly.GetType(className);
            var instance = (ISolution?)Activator.CreateInstance(targetType!);

            return instance!;
        }
    }
}
