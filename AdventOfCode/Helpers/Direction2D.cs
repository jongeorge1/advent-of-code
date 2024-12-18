#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace AdventOfCode.Helpers;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

[DebuggerDisplay("{Name}")]
public class Direction2D
{
    public static readonly Direction2D North = new() { Name = "North", Vector = (0, -1), Orientation = Orientations.Vertical };
    public static readonly Direction2D South = new() { Name = "South", Vector = (0, 1), Orientation = Orientations.Vertical };
    public static readonly Direction2D East = new() { Name = "East", Vector = (1, 0), Orientation = Orientations.Horizontal };
    public static readonly Direction2D West = new() { Name = "West", Vector = (-1, 0), Orientation = Orientations.Horizontal };

    public static readonly ImmutableArray<Direction2D> All = [North, South, East, West];

    public static readonly ImmutableDictionary<Orientations, Direction2D[]> AllByOrientation = new Dictionary<Orientations, Direction2D[]>()
    {
        [Orientations.Horizontal] = [East, West],
        [Orientations.Vertical] = [North, South],
    }.ToImmutableDictionary();

    public static readonly ImmutableDictionary<char, Direction2D> ArrowToDirectionMap = new Dictionary<char, Direction2D>()
    {
        ['^'] = North,
        ['v'] = South,
        ['>'] = East,
        ['<'] = West,
    }.ToImmutableDictionary();

    public static readonly ImmutableDictionary<char, Direction2D> UpDownLeftRightToDirectionMap = new Dictionary<char, Direction2D>()
    {
        ['U'] = North,
        ['D'] = South,
        ['L'] = West,
        ['R'] = East,
    }.ToImmutableDictionary();

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

    public Orientations PerpendicularOrientation => GetPerpendicularOrientation(this.Orientation);

    public Direction2D Left { get; set; }

    public Direction2D Right { get; set; }

    public static Orientations GetPerpendicularOrientation(Orientations orientation) => orientation == Orientations.Vertical ? Orientations.Horizontal : Orientations.Vertical;

    public (int X, int Y) GetNextLocation((int X, int Y) currentLocation) => (currentLocation.X + this.Vector.DX, currentLocation.Y + this.Vector.DY);
}
