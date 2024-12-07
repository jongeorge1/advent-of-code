namespace AdventOfCode.Year2020.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private static readonly Dictionary<string, Func<(int, int, int), (int, int, int)>> Mutators = new()
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
            Dictionary<(int X, int Y, int Z), bool>? flippedTiles = Setup(input);

            for (int generation = 0; generation < 100; generation++)
            {
                IEnumerable<(int, int, int)>? tilesToCheck = flippedTiles.Keys.SelectMany(t => GetNeighbours(t)).Distinct();
                var newGrid = new Dictionary<(int, int, int), bool>();

                foreach ((int, int, int) current in tilesToCheck)
                {
                    IEnumerable<(int, int, int)> neighbours = GetNeighbours(current);
                    int adjacentBlackTiles = neighbours.Count(n => flippedTiles.ContainsKey(n));
                    bool currentIsBlack = flippedTiles.ContainsKey(current);

                    if (currentIsBlack && (adjacentBlackTiles == 0 || adjacentBlackTiles > 2))
                    {
                        // Current location is black and needs to go white; we don't add it to the new grid
                        // (so this is a noop)
                    }
                    else if (!currentIsBlack && adjacentBlackTiles == 2)
                    {
                        // Current is white and needs to go black.
                        newGrid.Add(current, true);
                    }
                    else if (currentIsBlack)
                    {
                        // No change...
                        newGrid.Add(current, true);
                    }
                }

                flippedTiles = newGrid;
            }

            return flippedTiles.Count.ToString();
        }

        private static IEnumerable<(int X, int Y, int Z)> GetNeighbours((int X, int Y, int Z) location)
        {
            return Mutators.Select(m => m.Value(location));
        }

        private static Dictionary<(int X, int Y, int Z), bool> Setup(string[] input)
        {
            // Represent the hex grid using cube coordinate system; see https://www.redblobgames.com/grids/hexagons/
            // for more info.
            var flippedTiles = new Dictionary<(int, int, int), bool>();

            foreach (string direction in input)
            {
                (int, int, int) location = GetLocation(direction);
                if (flippedTiles.ContainsKey(location))
                {
                    flippedTiles.Remove(location);
                }
                else
                {
                    flippedTiles.Add(location, true);
                }
            }

            return flippedTiles;
        }

        private static (int X, int Y, int Z) GetLocation(string directions)
        {
            ReadOnlySpan<char> remainingDirections = directions.AsSpan();

            (int, int, int) location = (0, 0, 0);

            while (remainingDirections.Length > 0)
            {
                foreach (KeyValuePair<string, Func<(int, int, int), (int, int, int)>> mutator in Mutators)
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
