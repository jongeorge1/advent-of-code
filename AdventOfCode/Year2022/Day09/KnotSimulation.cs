namespace AdventOfCode.Year2022.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using AdventOfCode.Helpers;

    public static class KnotSimulation
    {
        private static readonly Dictionary<char, (int X, int Y)> Directions = new()
        {
            { 'U', (0, 1) },
            { 'D', (0, -1) },
            { 'L', (-1, 0) },
            { 'R', (1, 0) },
        };

        public static string Run(string[] input, int knotCount)
        {
            HashSet<(int X, int Y)> tailVisitedLocations = new()
            {
                (0, 0),
            };

            (int X, int Y)[] knots = new (int, int)[knotCount];

            foreach (string row in input)
            {
                var direction = Directions[row[0]];
                var times = int.Parse(row[2..]);

                for (int i = 0; i < times; ++i)
                {
                    knots[0] = (knots[0].X + direction.X, knots[0].Y + direction.Y);

                    for (int currentKnot = 1; currentKnot < knotCount; ++currentKnot)
                    {
                        // See if this knot needs to move to follow the previous one.
                        int currentKnotMoveX = 0;
                        int currentKnotMoveY = 0;

                        if (knots[currentKnot - 1].X == knots[currentKnot].X)
                        {
                            // the two are in the same row... are they two apart?
                            int distance = knots[currentKnot - 1].Y - knots[currentKnot].Y;
                            if (Math.Abs(distance) == 2)
                            {
                                currentKnotMoveY = distance / 2;
                            }
                        }
                        else if (knots[currentKnot - 1].Y == knots[currentKnot].Y)
                        {
                            // the two are in the same column.. are they two apart?
                            int distance = knots[currentKnot - 1].X - knots[currentKnot].X;
                            if (Math.Abs(distance) == 2)
                            {
                                currentKnotMoveX = distance / 2;
                            }
                        }
                        else if (Distance.Manhattan(knots[currentKnot - 1], knots[currentKnot]) > 2)
                        {
                            // separated diagonally, make a diagonal move towards the head
                            currentKnotMoveX = knots[currentKnot - 1].X > knots[currentKnot].X ? 1 : -1;
                            currentKnotMoveY = knots[currentKnot - 1].Y > knots[currentKnot].Y ? 1 : -1;
                        }

                        if (currentKnotMoveY != 0 || currentKnotMoveX != 0)
                        {
                            knots[currentKnot] = (knots[currentKnot].X + currentKnotMoveX, knots[currentKnot].Y + currentKnotMoveY);
                        }
                    }

                    tailVisitedLocations.Add(knots[^1]);
                }
            }

            return tailVisitedLocations.Count.ToString();
        }
    }
}
