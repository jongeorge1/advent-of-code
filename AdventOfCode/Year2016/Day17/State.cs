namespace AdventOfCode.Year2016.Day17
{
    public record State
    {
        public int X { get; init; }

        public int Y { get; init; }

        public int Steps { get; init; }

        public string? Path { get; init; }

        public AvailableDirections AvailableDirections { get; init; }

        public bool CanMove(AvailableDirections direction)
        {
            return (direction & this.AvailableDirections) == direction;
        }
    }
}
