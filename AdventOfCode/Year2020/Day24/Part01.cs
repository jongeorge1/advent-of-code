namespace AdventOfCode.Year2020.Day24
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        private static readonly Dictionary<string, Func<(int X, int Y, int Z), (int X, int Y, int Z)>> Mutators = new()
        {
            { "ne", ((int X, int Y, int Z) current) => (current.X + 1, current.Y, current.Z - 1) },
            { "nw", ((int X, int Y, int Z) current) => (current.X, current.Y + 1, current.Z - 1) },
            { "se", ((int X, int Y, int Z) current) => (current.X, current.Y - 1, current.Z + 1) },
            { "sw", ((int X, int Y, int Z) current) => (current.X - 1, current.Y, current.Z + 1) },
            { "e", ((int X, int Y, int Z) current) => (current.X + 1, current.Y - 1, current.Z) },
            { "w", ((int X, int Y, int Z) current) => (current.X - 1, current.Y + 1, current.Z) },
        };

        public string Solve(string[] input)
        {
            // Represent the hex grid using cube coordinate system; see https://www.redblobgames.com/grids/hexagons/
            // for more info.
            var flippedTiles = new Dictionary<(int X, int Y, int Z), bool>();

            foreach (string direction in input)
            {
                (int X, int Y, int Z) location = GetLocation(direction);
                if (flippedTiles.ContainsKey(location))
                {
                    flippedTiles.Remove(location);
                }
                else
                {
                    flippedTiles.Add(location, true);
                }
            }

            return flippedTiles.Count.ToString();
        }

        private static (int X, int Y, int Z) GetLocation(string directions)
        {
            ReadOnlySpan<char> remainingDirections = directions.AsSpan();

            (int X, int Y, int Z) location = (0, 0, 0);

            while (remainingDirections.Length > 0)
            {
                foreach (KeyValuePair<string, Func<(int X, int Y, int Z), (int X, int Y, int Z)>> mutator in Mutators)
                {
                    if (remainingDirections.StartsWith(mutator.Key))
                    {
                        location = mutator.Value(location);
                        remainingDirections = remainingDirections[mutator.Key.Length..];
                    }
                }
            }

            return location;
        }
    }
}
