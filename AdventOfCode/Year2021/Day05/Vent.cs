namespace AdventOfCode.Year2021.Day05
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("({Start.X}, {Start.Y}) -> ({End.X}, {End.Y})")]
    public class Vent
    {
        public Vent(string input)
        {
            string[] components = input.Split(new[] { ' ', ',' }, System.StringSplitOptions.None);

            int x1 = int.Parse(components[0]);
            int y1 = int.Parse(components[1]);
            int x2 = int.Parse(components[3]);
            int y2 = int.Parse(components[4]);

            this.IsDiagonal = x1 != x2 && y1 != y2;

            this.MinX = Math.Min(x1, x2);
            this.MaxX = Math.Max(x1, x2);

            this.MinY = Math.Min(y1, y2);
            this.MaxY = Math.Max(y1, y2);

            this.Start = x1 == this.MinX ? (x1, y1) : (x2, y2);
            this.End = x1 == this.MinX ? (x2, y2) : (x1, y1);

            if (this.IsDiagonal)
            {
                this.Gradient = this.Start.Y < this.End.Y ? 1 : -1;
                this.YOffset = this.Start.Y - (this.Start.X * this.Gradient);
            }
        }

        public bool IsDiagonal { get; }

        public int MinX { get; }

        public int MaxX { get; }

        public int MinY { get; }

        public int MaxY { get; }

        public (int X, int Y) Start { get; }

        public (int X, int Y) End { get; }

        public int? Gradient { get; }

        public int? YOffset { get; }

        public bool Crosses(int x, int y)
        {
            if (y < this.MinY || y > this.MaxY || x < this.MinX || x > this.MaxX)
            {
                return false;
            }

            if (this.IsDiagonal)
            {
                return y == (x * this.Gradient) + this.YOffset;
            }

            if (this.Start.X == this.End.X && x == this.Start.X)
            {
                return true;
            }

            if (this.Start.Y == this.End.Y && y == this.Start.Y)
            {
                return true;
            }

            return false;
        }
    }
}
