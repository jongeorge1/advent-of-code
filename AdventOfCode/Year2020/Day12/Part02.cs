namespace AdventOfCode.Year2020.Day12
{
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            (int X, int Y) waypointOffset = (10, 1);
            (int X, int Y) position = (0, 0);

            foreach (string current in input)
            {
                int quantifier = int.Parse(current[1..]);

                switch (current[0])
                {
                    case 'N':
                        waypointOffset = (waypointOffset.X, waypointOffset.Y + quantifier);
                        break;

                    case 'E':
                        waypointOffset = (waypointOffset.X + quantifier, waypointOffset.Y);
                        break;

                    case 'S':
                        waypointOffset = (waypointOffset.X, waypointOffset.Y - quantifier);
                        break;

                    case 'W':
                        waypointOffset = (waypointOffset.X - quantifier, waypointOffset.Y);
                        break;

                    case 'F':
                        position = Move(position, waypointOffset, quantifier);
                        break;

                    case 'L':
                        waypointOffset = Transform(waypointOffset, 360 - quantifier);
                        break;

                    case 'R':
                        waypointOffset = Transform(waypointOffset, quantifier);
                        break;
                }
            }

            return Distance.Manhattan(position.X, position.Y).ToString();
        }

        private static (int X, int Y) Move((int X, int Y) position, (int X, int Y) waypointOffset, int quantifier)
        {
            for (int i = 0; i < quantifier; i++)
            {
                position = (position.X + waypointOffset.X, position.Y + waypointOffset.Y);
            }

            return position;
        }

        private static (int X, int Y) Transform((int X, int Y) offset, int quantifier)
        {
            return quantifier switch
            {
                0 => offset,
                90 => (offset.Y, -offset.X),
                180 => (-offset.X, -offset.Y),
                270 => (-offset.Y, offset.X),
                _ => throw new Exception(),
            };
        }
    }
}
