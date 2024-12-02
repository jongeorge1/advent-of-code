﻿namespace AdventOfCode.Year2020.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Tile[] tiles = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Tile(x))
                .ToArray();

            // The tiles at the corners will have 2 unmatched edges, so we can identify them by looking for any tiles
            // that fit those criteria.
            var edgeMap = tiles.ToDictionary(x => x, x => tiles.Count(t => t.HasMatchingEdges(x)));
            Tile[] corners = edgeMap
                .Where(x => x.Value == 2)
                .Select(x => x.Key)
                .ToArray();

            // Now we're ready for the ubergrid. It's a square, so we can determine its dimensions from the input
            int size = (int)Math.Sqrt(tiles.Length);
            var ubergrid = new Tile[size, size];

            // Take the first corner, orient it until the non-matching edges are top and left, and add to the grid.
            Tile? firstCorner = corners.First();

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                }
            }

            return string.Empty;
        }

        private class Tile
        {
            public Tile(string input)
            {
                string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                this.Id = int.Parse(rows[0].Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);

                this.TileData = rows[1..];

                this.Edges = new List<TileEdge>()
                {
                    new TileEdge(this.TileData[0]),
                    new TileEdge(this.TileData[^1]),
                    new TileEdge(string.Join(string.Empty, this.TileData.Select(x => x[0]))),
                    new TileEdge(string.Join(string.Empty, this.TileData.Select(x => x[^1]))),
                };
            }

            public int Id { get; }

            public string[] TileData { get; }

            public List<TileEdge> Edges { get; }

            public bool HasMatchingEdges(Tile tile)
            {
                if (tile == this)
                {
                    return false;
                }

                return this.Edges.Any(myEdge => tile.Edges.Any(theirEdge => myEdge.IsMatch(theirEdge)));
            }
        }

        private class TileEdge
        {
            public TileEdge(string edge)
            {
                char[] inverse = edge.ToCharArray();
                Array.Reverse(inverse);

                this.Orientations = [edge, new string(inverse)];
            }

            public string[] Orientations { get; }

            public bool IsMatch(TileEdge edge)
            {
                return this.Orientations.Intersect(edge.Orientations).Any();
            }
        }
    }
}
