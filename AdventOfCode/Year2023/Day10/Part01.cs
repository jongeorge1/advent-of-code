namespace AdventOfCode.Year2023.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            (int X, int Y) startPosition = GetStartPositionCoordinates(input);

            char startTileType = GetStartTileType(input, startPosition);

            int stepsTaken = 0;

            (int X, int Y) currentPosition = startPosition;
            (int X, int Y) previousPosition = (-1, -1);

            do
            {
                (int, int) nextPosition = GetNextPosition(currentPosition, previousPosition, input[currentPosition.Y][currentPosition.X], startTileType);

                previousPosition = currentPosition;
                currentPosition = nextPosition;
                ++stepsTaken;
            }
            while (currentPosition != startPosition);

            return (stepsTaken / 2).ToString();
        }

        private static (int, int) GetNextPosition((int X, int Y) currentPosition, (int X, int Y) previousPosition, char tileType, char startTileType)
        {
            List<(int, int)> connectedTiles = new();

            if (tileType == TileType.Start)
            {
                tileType = startTileType;
            }

            if (TileType.NorthConnectedTiles.Contains(tileType))
            {
                connectedTiles.Add((currentPosition.X, currentPosition.Y - 1));
            }

            if (TileType.SouthConnectedTiles.Contains(tileType))
            {
                connectedTiles.Add((currentPosition.X, currentPosition.Y + 1));
            }

            if (TileType.EastConnectedTiles.Contains(tileType))
            {
                connectedTiles.Add((currentPosition.X + 1, currentPosition.Y));
            }

            if (TileType.WestConnectedTiles.Contains(tileType))
            {
                connectedTiles.Add((currentPosition.X - 1, currentPosition.Y));
            }

            return connectedTiles.First(x => x != previousPosition);
        }

        private static (int, int) GetStartPositionCoordinates(string[] input)
        {
            for (int y = 0; y < input.Length; y++)
            {
                int startIndex = input[y].IndexOf(TileType.Start);
                if (startIndex != -1)
                {
                    return (startIndex, y);
                }
            }

            throw new Exception("Start position not found");
        }

        private static char GetStartTileType(string[] input, (int X, int Y) startPosition)
        {
            char tileNorth = startPosition.Y > 0 ? input[startPosition.Y - 1][startPosition.X] : TileType.Ground;
            char tileSouth = startPosition.Y < input.Length - 1 ? input[startPosition.Y + 1][startPosition.X] : TileType.Ground;
            char tileWest = startPosition.X > 0 ? input[startPosition.Y][startPosition.X - 1] : TileType.Ground;
            char tileEast = startPosition.X < input[0].Length - 1 ? input[startPosition.Y][startPosition.X + 1] : TileType.Ground;

            if (TileType.SouthConnectedTiles.Contains(tileNorth))
            {
                if (TileType.NorthConnectedTiles.Contains(tileSouth))
                {
                    return TileType.NorthSouth;
                }
                else if (TileType.EastConnectedTiles.Contains(tileWest))
                {
                    return TileType.NorthWest;
                }
                else if (TileType.WestConnectedTiles.Contains(tileEast))
                {
                    return TileType.NorthEast;
                }
                else
                {
                    throw new Exception("Bad assumption");
                }
            }
            else if (TileType.NorthConnectedTiles.Contains(tileSouth))
            {
                if (TileType.EastConnectedTiles.Contains(tileWest))
                {
                    return TileType.SouthWest;
                }
                else if (TileType.WestConnectedTiles.Contains(tileEast))
                {
                    return TileType.SouthEast;
                }
                else
                {
                    throw new Exception("Bad assumption");
                }
            }
            else
            {
                if (TileType.EastConnectedTiles.Contains(tileWest) && TileType.WestConnectedTiles.Contains(tileEast))
                {
                    return TileType.EastWest;
                }
                else
                {
                    throw new Exception("Bad assumption");
                }
            }
        }

        private static class TileType
        {
            public const char NorthSouth = '|';

            public const char EastWest = '-';

            public const char NorthEast = 'L';

            public const char NorthWest = 'J';

            public const char SouthWest = '7';

            public const char SouthEast = 'F';

            public const char Ground = '.';

            public const char Start = 'S';

            public static char[] NorthConnectedTiles = { NorthSouth, NorthEast, NorthWest };

            public static char[] SouthConnectedTiles = { NorthSouth, SouthEast, SouthWest };

            public static char[] EastConnectedTiles = { NorthEast, SouthEast, EastWest };

            public static char[] WestConnectedTiles = { NorthWest, SouthWest, EastWest };
        }
    }
}