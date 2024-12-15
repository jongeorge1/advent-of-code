namespace AdventOfCode.Year2024.Day13;

using System;
using System.Linq;
using static AdventOfCode.Helpers.Numeric;

public class ClawMachine
{
    public ClawMachine(string[] input, long prizeOffset)
    {
        if (input.Length != 3)
        {
            throw new ArgumentException("Input should contain three entries");
        }

        this.ButtonA = new Button(input[0]);
        this.ButtonB = new Button(input[1]);

        string[] prizeLocation = input[2].Split(['=', ',']);
        this.PrizeLocation = (prizeOffset + int.Parse(prizeLocation[1]), prizeOffset + int.Parse(prizeLocation[3]));
    }

    public Button ButtonA { get; }

    public Button ButtonB { get; }

    public (long X, long Y) PrizeLocation { get; }

    public static ClawMachine[] FromInput(string[] input, long prizeOffset = 0)
    {
        return input.Chunk(4).Select(rows => new ClawMachine(rows[0..3], prizeOffset)).ToArray();
    }

    public long GetCostOfCheapestSolution()
    {
        decimal buttonBPresses = (decimal)((this.ButtonA.DeltaX * this.PrizeLocation.Y) - (this.ButtonA.DeltaY * this.PrizeLocation.X)) /
                              ((this.ButtonA.DeltaX * this.ButtonB.DeltaY) - (this.ButtonA.DeltaY * this.ButtonB.DeltaX));

        decimal buttonAPresses = (decimal)(this.PrizeLocation.X - (buttonBPresses * this.ButtonB.DeltaX)) / this.ButtonA.DeltaX;

        if (buttonAPresses >= 0 && buttonAPresses == Math.Floor(buttonAPresses) && buttonBPresses >= 0 && buttonBPresses == Math.Floor(buttonBPresses))
        {
            return (long)((buttonAPresses * 3) + buttonBPresses);
        }

        return 0;
    }

    public readonly record struct Button
    {
        public Button(string input)
        {
            string[] elements = input.Split(['+', ','], StringSplitOptions.RemoveEmptyEntries);
            this.ButtonName = input[7];
            this.DeltaX = int.Parse(elements[1]);
            this.DeltaY = int.Parse(elements[3]);
        }

        public char ButtonName { get; }

        public int DeltaX { get; }

        public int DeltaY { get; }
    }
}