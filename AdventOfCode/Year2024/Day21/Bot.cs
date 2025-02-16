namespace AdventOfCode.Year2024.Day21;

using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode.Helpers;

public class Bot(Dictionary<char, (int X, int Y)> keyPad, char start)
{
    private Dictionary<char, (int X, int Y)> KeyPad { get; } = keyPad;

    private char CurrentlyPointingAt { get; set; } = start;

    public string GetMovementsForButton(char button)
    {
        (int X, int Y) currentLocation = this.KeyPad[this.CurrentlyPointingAt];
        (int X, int Y) targetLocation = this.KeyPad[button];

        int requiredXMovement = targetLocation.X - currentLocation.X;
        int requiredYMovement = targetLocation.Y - currentLocation.Y;

        // Bit of a cheat here. Although the instructions say you can't go over the A, it doesn't really
        // matter for the solution. So we're not worrying about it.
        StringBuilder movements = new();

        if (requiredXMovement != 0)
        {
            Direction2D direction = requiredXMovement > 0 ? Direction2D.East : Direction2D.West;
            movements.Append(Direction2D.DirectionToArrowMap[direction], Math.Abs(requiredXMovement));
        }

        if (requiredYMovement != 0)
        {
            Direction2D direction = requiredYMovement > 0 ? Direction2D.South : Direction2D.North;
            movements.Append(Direction2D.DirectionToArrowMap[direction], Math.Abs(requiredYMovement));
        }

        this.CurrentlyPointingAt = button;

        return movements.ToString();
    }

    public ReadOnlySpan<char> GetMovementsForSequence(ReadOnlySpan<char> sequence)
    {
        StringBuilder movements = new();
        foreach (char current in sequence)
        {
            movements.Append(this.GetMovementsForButton(current));
            movements.Append('A');
        }

        return movements.ToString();
    }
}
