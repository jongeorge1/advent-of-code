namespace AdventOfCode.Year2022.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Xml.Schema;
    using AdventOfCode;
    using AdventOfCode.Helpers;
    using NUnit.Framework;

    public class Part01 : ISolution
    {
        private static readonly (int dX, int dY)[] DirectionOffsets = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        public string Solve(string input)
        {
            Dictionary<int, List<(int StartColumn, int Direction)>> horizontalBlizzards = new();
            Dictionary<int, List<(int StartRow, int Direction)>> verticalBlizzards = new();

            StringExtensions.StringSplitEnumerator mapRows = input.SplitLines();

            // Skip the first row as it's "wall". This will also apply to the first column.
            mapRows.MoveNext();

            // However, we can use it to establish the "width" of the play area...
            int width = mapRows.Current.Line.Length - 2;

            int row = 0;

            while (mapRows.MoveNext() && mapRows.Current.Line[1] != '#')
            {
                horizontalBlizzards[row] = new List<(int StartColumn, int Direction)>();

                for (int column = 0; column < mapRows.Current.Line.Length - 1; ++column)
                {
                    if (row == 0)
                    {
                        verticalBlizzards[column] = new List<(int StartRow, int Direction)>();
                    }

                    switch (mapRows.Current.Line[column + 1])
                    {
                        case '>':
                            horizontalBlizzards[row].Add((column, 1));
                            break;
                        case '<':
                            horizontalBlizzards[row].Add((column, -1));
                            break;
                        case '^':
                            verticalBlizzards[column].Add((row, -1));
                            break;
                        case 'v':
                            verticalBlizzards[column].Add((row, 1));
                            break;
                        default:
                            // no-op
                            break;
                    }
                }

                ++row;
            }

            int height = row;

            var targetLocation = (width - 1, height - 1);

            // There will be multiple possible moves at any point, so we need the standard BFS to find the path.
            Queue<(int Time, (int X, int Y) Location)> searchQueue = new();
            HashSet<(int Time, (int X, int Y) Location)> seenLocations = new();

            int seenLocationsTimeModulo = width * height;

            searchQueue.Enqueue((0, (0, -1)));

            while (searchQueue.Count > 0)
            {
                (int time, (int X, int Y) location) = searchQueue.Dequeue();

                ++time;

                if (location == targetLocation)
                {
                    return time.ToString();
                }

                int currentLocationTimeModulo = time % seenLocationsTimeModulo;

                if (seenLocations.Contains((currentLocationTimeModulo, location)))
                {
                    continue;
                }

                seenLocations.Add((currentLocationTimeModulo, location));

                foreach ((int dX, int dY) current in DirectionOffsets)
                {
                    (int X, int Y) potentialLocation = (location.X + current.dX, location.Y + current.dY);

                    // Is this a valid location?
                    if (potentialLocation.X < 0 || potentialLocation.X >= width || potentialLocation.Y < 0 || potentialLocation.Y >= height)
                    {
                        continue;
                    }

                    // Is there going to be a blizzard there at this point?
                    bool nextLocationWillBeInBlizzard = LocationWillBeInHorizontallyMovingBlizzardAtTime(potentialLocation, horizontalBlizzards, time, width)
                        || LocationWillBeInVerticallyMovingBlizzardAtTime(potentialLocation, verticalBlizzards, time, height);

                    if (!nextLocationWillBeInBlizzard)
                    {
                        // This move is good.
                        searchQueue.Enqueue((time, potentialLocation));
                    }
                }

                // Staying still is an option if the current location won't be in the blizzard at the new time.
                bool currentLocationWillBeInBlizzard = LocationWillBeInHorizontallyMovingBlizzardAtTime(location, horizontalBlizzards, time, width)
                    || LocationWillBeInVerticallyMovingBlizzardAtTime(location, verticalBlizzards, time, height);
                if (!currentLocationWillBeInBlizzard)
                {
                    searchQueue.Enqueue((time, location));
                }
            }

            return string.Empty;
        }

        private static bool LocationWillBeInVerticallyMovingBlizzardAtTime(
            (int X, int Y) potentialLocation,
            Dictionary<int, List<(int StartRow, int Direction)>> verticalBlizzards,
            int time,
            int height)
        {
            if (potentialLocation.Y == -1)
            {
                return false;
            }

            int adjustedSteps = time % height;

            foreach ((int StartRow, int Direction) verticalBlizzard in verticalBlizzards[potentialLocation.X])
            {
                int endRow = (height + verticalBlizzard.StartRow + (adjustedSteps * verticalBlizzard.Direction)) % height;
                if (endRow == potentialLocation.Y)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool LocationWillBeInHorizontallyMovingBlizzardAtTime(
            (int X, int Y) potentialLocation,
            Dictionary<int, List<(int StartColumn, int Direction)>> horizontalBlizzards,
            int time,
            int width)
        {
            if (potentialLocation.Y == -1)
            {
                return false;
            }

            int adjustedSteps = time % width;

            foreach ((int StartColumn, int Direction) horizontalBlizzard in horizontalBlizzards[potentialLocation.Y])
            {
                int endColumn = (width + horizontalBlizzard.StartColumn + (adjustedSteps * horizontalBlizzard.Direction)) % width;
                if (endColumn == potentialLocation.X)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
