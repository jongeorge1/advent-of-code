namespace AdventOfCode.Year2021.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            HashSet<(int X, int Y)> dots = new HashSet<(int, int)>(input.Select(x => x.Split(',')).Select(x => (int.Parse(x[0]), int.Parse(x[1]))));
            List<(string Axis, int Position)> folds = input[1].Split(Environment.NewLine).Select(x => x.Split([' ', '='])).Select(x => (x[^2], int.Parse(x[^1]))).ToList();

            foreach ((string Axis, int Position) fold in folds)
            {
                HashSet<(int, int)> results = new();

                if (fold.Axis == "x")
                {
                    foreach ((int X, int Y) current in dots)
                    {
                        if (current.X > fold.Position)
                        {
                            int newX = fold.Position - (current.X - fold.Position);
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
                        if (current.Y > fold.Position)
                        {
                            int newY = fold.Position - (current.Y - fold.Position);
                            results.Add((current.X, newY));
                        }
                        else
                        {
                            results.Add(current);
                        }
                    }
                }

                dots = results;
            }

            // Now we need to output the result so it can be read.
            int maxX = dots.Max(x => x.X);
            int maxY = dots.Max(x => x.Y);

            StringBuilder output = new();

            for (int y = 0; y <= maxY; ++y)
            {
                for (int x = 0; x <= maxX; ++x)
                {
                    output.Append(dots.Contains((x, y)) ? "#" : " ");
                }

                output.AppendLine();
            }

            return output.ToString();
        }
    }
}
