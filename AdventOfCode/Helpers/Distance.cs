namespace AdventOfCode.Helpers
{
    using System;
    using System.Drawing;

    public static class Distance
    {
        public static int Manhattan(int x, int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        public static int Manhattan(int x, int y, int z)
        {
            return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        }

        public static int Manhattan((int X, int Y, int Z) position) => Manhattan(position.X, position.Y, position.Z);

        public static int Manhattan(Point point) => Manhattan(point.X, point.Y);

        public static int Manhattan((int X, int Y, int Z) first, (int X, int Y, int Z) second)
        {
            return Math.Abs(second.X - first.X) + Math.Abs(second.Y - first.Y) + Math.Abs(second.Z - first.Z);
        }
    }
}
