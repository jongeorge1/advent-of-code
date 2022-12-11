namespace AdventOfCode.Year2022.Day10
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var device = new Device(input);
            int signalStrength = 0;

            device.Tick += (object? sender, TickEventArgs args) =>
            {
                if ((args.Cycle - 20) % 40 == 0)
                {
                    signalStrength += args.Cycle * ((Device)sender).X;
                }
            };

            device.Execute();

            return signalStrength.ToString();
        }
    }
}
