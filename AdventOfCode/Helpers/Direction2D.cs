namespace AdventOfCode.Helpers;

public class Direction2D
{
    public static readonly Direction2D North = new Direction2D { Vector = (0, -1) };
    public static readonly Direction2D South = new Direction2D { Vector = (0, 1) };
    public static readonly Direction2D East = new Direction2D { Vector = (1, 0) };
    public static readonly Direction2D West = new Direction2D { Vector = (-1, 0) };

    static Direction2D()
    {
        North.Left = West;
        North.Right = East;
        South.Left = East;
        South.Right = West;
        East.Left = North;
        East.Right = South;
        West.Left = South;
        West.Right = North;
    }

    public (int DX, int DY) Vector { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Direction2D Left { get; set; }

    public Direction2D Right { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public (int X, int Y) GetNextLocation((int X, int Y) currentLocation) => (currentLocation.X + this.Vector.DX, currentLocation.Y + this.Vector.DY);
}
