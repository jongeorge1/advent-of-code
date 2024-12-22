using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

IConfig config = new DebugInProcessConfig();

int year = int.Parse(args[0]);
int day = int.Parse(args[1]);

int? part = args.Length == 3 ? int.Parse(args[2]) : null;

string className = part.HasValue
    ? $"AdventOfCode.Benchmarks.Year{year:D4}Day{day:D2}Part{part:D2}"
    : $"AdventOfCode.Benchmarks.Year{year:D4}Day{day:D2}";

Type ? targetType = typeof(Program).Assembly.GetType(className);

if (targetType is null)
{
    throw new NotImplementedException("The requested benchmark does not exist");
}

BenchmarkRunner.Run(targetType, config);
