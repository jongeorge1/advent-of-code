namespace AdventOfCode.Year2020.Day12
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<int, Func<(int, int), (int, int)>> Mutators =
            new()
            {
                { 0, input => (input.Item1, input.Item2 + 1) },
                { 180, input => (input.Item1, input.Item2 - 1) },
                { 90, input => (input.Item1 + 1, input.Item2) },
                { 270, input => (input.Item1 - 1, input.Item2) },
            };

        public string Solve(string[] input)
        {
            int currentBearing = 90;
            (int, int) position = (0, 0);

            foreach (string current in input)
            {
                int? movementBearing = null;
                int quantifier = int.Parse(current[1..]);

                switch (current[0])
                {
                    case 'N':
                        movementBearing = 0;
                        break;

                    case 'E':
                        movementBearing = 90;
                        break;

                    case 'S':
                        movementBearing = 180;
                        break;

                    case 'W':
                        movementBearing = 270;
                        break;

                    case 'F':
                        movementBearing = currentBearing;
                        break;

                    case 'L':
                        currentBearing = (currentBearing + 360 - quantifier) % 360;
                        break;

                    case 'R':
                        currentBearing = (currentBearing + quantifier) % 360;
                        break;
                }

                if (movementBearing.HasValue)
                {
                    for (int i = 0; i < quantifier; i++)
                    {
                        position = Mutators[movementBearing.Value](position);
                    }
                }
            }

            return Distance.Manhattan(position.Item1, position.Item2).ToString();
        }
    }
}
