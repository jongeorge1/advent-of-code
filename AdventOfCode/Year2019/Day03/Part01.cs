namespace AdventOfCode.Year2019.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<char, Point> Vectors = new()
        {
            { 'U', new Point(0, 1) },
            { 'D', new Point(0, -1) },
            { 'L', new Point(-1, 0) },
            { 'R', new Point(1, 0) },
        };

        public string Solve(string[] input)
        {
            List<Point>[] lines = input
                .Select(path => path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                .Select(path => this.BuildPoints(path))
                .ToArray();

            // Find common points
            IEnumerable<Point> commonPoints = lines[0].Intersect(lines[1]);
            int shortestDistance = commonPoints.Min(x => this.DistanceFromCentralPort(x));

            return shortestDistance.ToString();
        }

        private int DistanceFromCentralPort(Point point)
        {
            return Distance.Manhattan(point);
        }

        private List<Point> BuildPoints(string[] path)
        {
            var result = new List<Point>(200000);
            var currentLocation = new Point(0, 0);

            foreach (string current in path)
            {
                int distance = int.Parse(current.Substring(1));

                Point vector = Vectors[current[0]];

                for (int i = 0; i < distance; i++)
                {
                    currentLocation = new Point(currentLocation.X + vector.X, currentLocation.Y + vector.Y);
                    result.Add(currentLocation);
                }
            }

            return result;
        }
    }
}
