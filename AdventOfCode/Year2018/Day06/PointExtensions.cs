﻿namespace AdventOfCode.Year2018.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public static class PointExtensions
    {
        public static int ManhattanDistanceFrom(this Point point, Point other)
        {
            return Math.Abs(point.X - other.X) + Math.Abs(point.Y - other.Y);
        }

        public static Point[] FindClosest(this Point origin, IEnumerable<Point> points)
        {
            (Point Point, int Distance)[] distances = points.Select(x => (x, x.ManhattanDistanceFrom(origin))).ToArray();
            int closestDistance = distances.Min(x => x.Distance);
            return distances.Where(x => x.Distance == closestDistance).Select(x => x.Point).ToArray();
        }

        public static IEnumerable<Point> BuildRangeTo(this Point start, Point finish)
        {
            for (int x = start.X; x <= finish.X; x++)
            {
                for (int y = start.Y; y <= finish.Y; y++)
                {
                    yield return new Point { X = x, Y = y };
                }
            }
        }
    }
}
