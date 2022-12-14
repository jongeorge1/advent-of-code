using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

IConfig config = new DebugInProcessConfig();

int year = int.Parse(args[0]);
int day = int.Parse(args[1]);
int part = int.Parse(args[2]);

string className = $"AdventOfCode.Benchmarks.Year{year:D4}Day{day:D2}Part{part:D2}";
Type? targetType = typeof(Program).Assembly.GetType(className);

if (targetType is null)
{
    throw new NotImplementedException("The requested benchmark does not exist");
}

BenchmarkRunner.Run(targetType, config);
