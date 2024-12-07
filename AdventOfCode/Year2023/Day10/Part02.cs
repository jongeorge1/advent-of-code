namespace AdventOfCode.Year2023.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            (int X, int Y) startPosition = GetStartPositionCoordinates(input);

            char startTileType = GetStartTileType(input, startPosition);

            Dictionary<(int X, int Y), char> loopTiles = BuildLoopMap(input, startPosition, startTileType);

            // Now we're going to walk each line of the map and try and work out the cells that are
            // inside or outside.
            int insideLocations = 0;

            for (int y = 0; y < input.Length; ++y)
            {
                bool currentlyInside = false;
                bool currentlyOnHorizontalWall = false;
                char currentWallEntryTile = TileType.Ground;
                string currentLine = input[y];

                for (int x = 0; x < currentLine.Length; ++x)
                {
                    if (loopTiles.TryGetValue((x, y), out char tileType))
                    {
                        // We're transitioning over part of the pipe. Whether we count it as a change from inside
                        // to outside or vice versa depends on what type of pipe it is.
                        if (tileType == TileType.NorthSouth)
                        {
                            // This is the simplest case
                            currentlyInside = !currentlyInside;
                        }
                        else if (tileType == TileType.NorthEast ||
                            tileType == TileType.NorthWest ||
                            tileType == TileType.SouthEast ||
                            tileType == TileType.SouthWest)
                        {
                            // We're at the start or end of a wall
                            if (currentlyOnHorizontalWall)
                            {
                                // It's the end. If the wall started via a corner from the same direction
                                // it ends with, then we don't flip the inside/outside switch. Otherwise we do.
                                // Since we're going L-R we know that we would have entered from a NE or SE tile
                                // and will be leaving on a NW or SW
                                char curentTile = input[y][x];
                                if ((currentWallEntryTile == TileType.NorthEast && curentTile == TileType.SouthWest)
                                    || (currentWallEntryTile == TileType.SouthEast && curentTile == TileType.NorthWest))
                                {
                                    currentlyInside = !currentlyInside;
                                }

                                currentlyOnHorizontalWall = false;
                            }
                            else
                            {
                                currentlyOnHorizontalWall = true;
                                currentWallEntryTile = input[y][x];
                            }
                        }
                    }
                    else if (currentlyInside)
                    {
                        ++insideLocations;
                    }
                }
            }

            return insideLocations.ToString();
        }

        private static Dictionary<(int X, int Y), char> BuildLoopMap(string[] input, (int X, int Y) startPosition, char startTileType)
        {
            Dictionary<(int X, int Y), char> loopTiles = [];

            (int X, int Y) currentPosition = startPosition;
            char currentTileType = startTileType;
            (int X, int Y) previousPosition = (-1, -1);

            // Rather than count the steps, this loop is now used to identify the positions of tiles that are
            // in the loop
            do
            {
                (int, int) nextPosition = GetNextPosition(currentPosition, previousPosition, currentTileType);

                previousPosition = currentPosition;
                currentPosition = nextPosition;
                currentTileType = input[currentPosition.Y][currentPosition.X];
                loopTiles[currentPosition] = currentTileType;
            }
            while (currentPosition != startPosition);

            return loopTiles;
        }

        private static (int X, int Y) GetNextPosition((int X, int Y) currentPosition, (int X, int Y) previousPosition, char tileType)
        {
            List<(int, int)> connectedTiles = [];

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

        private static (int X, int Y) GetStartPositionCoordinates(string[] input)
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

            public static readonly char[] NorthConnectedTiles = [NorthSouth, NorthEast, NorthWest];

            public static readonly char[] SouthConnectedTiles = [NorthSouth, SouthEast, SouthWest];

            public static readonly char[] EastConnectedTiles = [NorthEast, SouthEast, EastWest];

            public static readonly char[] WestConnectedTiles = [NorthWest, SouthWest, EastWest];
        }
    }
}
