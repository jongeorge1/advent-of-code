namespace AdventOfCode.Year2024.Day13;

using System;
using System.Linq;

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

    public int GetCostOfCheapestSolution()
    {
        int minimumCost = int.MaxValue;

        for (int buttonAPresses = 0; buttonAPresses < 100; ++buttonAPresses)
        {
            for (int buttonBPresses = 0; buttonBPresses < 100; ++buttonBPresses)
            {
                if (this.PrizeLocation.X == (buttonAPresses * this.ButtonA.DeltaX) + (buttonBPresses * this.ButtonB.DeltaX) &&
                    this.PrizeLocation.Y == (buttonAPresses * this.ButtonA.DeltaY) + (buttonBPresses * this.ButtonB.DeltaY))
                {

                    int cost = (3 * buttonAPresses) + buttonBPresses;
                    minimumCost = Math.Min(minimumCost, cost);
                }
            }
        }

        return minimumCost == int.MaxValue ? 0 : minimumCost;
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