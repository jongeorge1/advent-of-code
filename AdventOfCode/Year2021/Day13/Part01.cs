namespace AdventOfCode.Year2021.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine + Environment.NewLine);
            List<(int X, int Y)> dots = components[0].Split(Environment.NewLine).Select(x => x.Split(',')).Select(x => (int.Parse(x[0]), int.Parse(x[1]))).ToList();
            List<(string Axis, int Position)> folds = components[1].Split(Environment.NewLine).Select(x => x.Split(new[] { ' ', '=' })).Select(x => (x[^2], int.Parse(x[^1]))).ToList();

            // We're only doing one fold.
            HashSet<(int, int)> results = new ();

            (string Axis, int Position) firstFold = folds[0];
            if (firstFold.Axis == "x")
            {
                foreach ((int X, int Y) current in dots)
                {
                    if (current.X > firstFold.Position)
                    {
                        int newX = firstFold.Position - (current.X - firstFold.Position);
                        results.Add((newX, current.Y));
                    }
                    else
                    {
                        results.Add(current);
                    }
                }
            }
            else
            {
                foreach ((int X, int Y) current in dots)
                {
                    if (current.Y > firstFold.Position)
                    {
                        int newY = firstFold.Position - (current.Y - firstFold.Position);
                        results.Add((current.X, newY));
                    }
                    else
                    {
                        results.Add(current);
                    }
                }
            }

            return results.Count.ToString();
        }
    }
}
