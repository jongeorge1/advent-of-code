namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class Program
    {
        private static void Main(string[] args)
        {
            int year = int.Parse(args[0]);
            int day = int.Parse(args[1]);
            int part = int.Parse(args[2]);
            int executions = args.Length > 3 ? int.Parse(args[3]) : 1;
            string[] data = args.Length > 4 ? args[4].Split(Environment.NewLine) : new string[0];

            // Load the data
            if (data.Length == 0)
            {
                var locationUri = new UriBuilder(Assembly.GetExecutingAssembly().Location!);
                string location = Uri.UnescapeDataString(locationUri.Path);
                string locationDirectory = Path.GetDirectoryName(location)!;
                string inputFileName = Path.Combine(locationDirectory, $"Year{year.ToString("D4")}", $"Day{day:D2}", "input.txt");
                data = File.ReadAllLines(inputFileName);
            }

            string result = string.Empty;
            var times = new List<double>(executions);

            if (executions > 1)
            {
                // This is a timing run, so warm things up first.
                for (int warmup = 0; warmup < 5; warmup++)
                {
                    ISolution instance = SolutionFactory.GetSolution(year, day, part);
                    instance.Solve(data);
                }
            }

            for (int i = 0; i < executions; i++)
            {
                ISolution instance = SolutionFactory.GetSolution(year, day, part);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                result = instance.Solve(data);

                stopwatch.Stop();

                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine(result);

            if (executions == 1)
            {
                Console.WriteLine($"Result obtained in {times[0]}ms");
            }
            else
            {
                Console.WriteLine($"Processing executed {executions} times:");
                Console.WriteLine($"\tAvg: {times.Average():0.0000}ms");
                Console.WriteLine($"\tMin: {times.Min():0.0000}ms");
                Console.WriteLine($"\tMax: {times.Max():0.0000}ms");
            }
        }
    }
}
