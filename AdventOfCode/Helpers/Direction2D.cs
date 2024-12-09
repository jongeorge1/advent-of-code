#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace AdventOfCode.Helpers;

using System.Diagnostics;

[DebuggerDisplay("{Name}")]
public class Direction2D
{
    public static readonly Direction2D North = new() { Name = "North", Vector = (0, -1), Orientation = Orientations.Vertical };
    public static readonly Direction2D South = new() { Name = "South", Vector = (0, 1), Orientation = Orientations.Vertical };
    public static readonly Direction2D East = new() { Name = "East", Vector = (1, 0), Orientation = Orientations.Horizontal };
    public static readonly Direction2D West = new() { Name = "West", Vector = (-1, 0), Orientation = Orientations.Horizontal };

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

    private Direction2D()
    {
    }

    public enum Orientations
    {
        Horizontal,
        Vertical,
    }

    public required string Name { get; init; }

    public required (int DX, int DY) Vector { get; init; }

    public required Orientations Orientation { get; init; }

    public Direction2D Left { get; set; }

    public Direction2D Right { get; set; }

    public (int X, int Y) GetNextLocation((int X, int Y) currentLocation) => (currentLocation.X + this.Vector.DX, currentLocation.Y + this.Vector.DY);
}
