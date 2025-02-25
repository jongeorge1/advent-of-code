﻿namespace AdventOfCode.Year2021.Day05
{
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Vent[] vents = input.Select(x => new Vent(x)).ToArray();

            // Find the problem space
            (int X, int Y) bottomLeft = (vents.Min(x => x.MinX), vents.Min(x => x.MinY));
            (int X, int Y) topRight = (vents.Max(x => x.MaxX), vents.Max(x => x.MaxY));

            int pointsWithIntersections = 0;

            for (int x = bottomLeft.X; x <= topRight.X; ++x)
            {
                for (int y = bottomLeft.Y; y <= topRight.Y; ++y)
                {
                    int intersectingLines = 0;
                    foreach (Vent v in vents)
                    {
                        if (v.Crosses(x, y))
                        {
                            ++intersectingLines;
                        }

                        if (intersectingLines > 1)
                        {
                            break;
                        }
                    }

                    if (intersectingLines > 1)
                    {
                        ++pointsWithIntersections;
                    }
                }
            }

            return pointsWithIntersections.ToString();
        }
    }
}
