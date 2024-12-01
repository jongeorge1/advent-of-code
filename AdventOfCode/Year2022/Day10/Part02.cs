namespace AdventOfCode.Year2022.Day10
{
    using System;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var device = new Device(input);

            device.Tick += (object? sender, TickEventArgs args) =>
            {
                int col = (args.Cycle - 1) % 40;

                // Draw the thing
                if (col >= device.X - 1 && col <= device.X + 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }

                if (col == 39)
                {
                    Console.WriteLine();
                }
            };

            device.Execute();

            return string.Empty;
        }
    }
}
