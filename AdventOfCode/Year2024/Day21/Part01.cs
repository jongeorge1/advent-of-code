namespace AdventOfCode.Year2024.Day21;

using System;
using System.Text;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        DoorBot doorBot = new();
        DirectionBot[] directionBots = [new DirectionBot(), new DirectionBot()];
        int totalComplexity = 0;

        for (int i = 0; i < input.Length; ++i)
        {
            ReadOnlySpan<char> currentSequence = doorBot.GetMovementsForSequence(input[i]);

            foreach (DirectionBot directionBot in directionBots)
            {
                currentSequence = directionBot.GetMovementsForSequence(currentSequence);
            }

            int sequenceLength = currentSequence.Length;
            int numericPartOfCode = int.Parse(input[i][0..^1]);
            totalComplexity += sequenceLength * numericPartOfCode;
        }

        return totalComplexity.ToString();
    }
}
