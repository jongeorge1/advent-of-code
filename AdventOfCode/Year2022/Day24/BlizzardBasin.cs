namespace AdventOfCode.Year2022.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Helpers;

    public class BlizzardBasin
    {
        private static readonly (int dX, int dY)[] DirectionOffsets = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        private Dictionary<int, List<(int StartColumn, int Direction)>> horizontalBlizzards = new();
        private Dictionary<int, List<(int StartRow, int Direction)>> verticalBlizzards = new();
        private int width;
        private int height;

        public BlizzardBasin(string[] map)
        {
            // Skip the first row as it's "wall". This will also apply to the first column.
            int currentRow = 1;

            // However, we can use it to establish the "width" of the play area...
            this.width = map[currentRow].Length - 2;

            while (currentRow < map.Length && map[currentRow][1] != '#')
            {
                this.horizontalBlizzards[currentRow - 1] = new List<(int StartColumn, int Direction)>();

                for (int column = 0; column < map[currentRow].Length - 1; ++column)
                {
                    if (currentRow == 1)
                    {
                        this.verticalBlizzards[column] = new List<(int StartRow, int Direction)>();
                    }

                    switch (map[currentRow][column + 1])
                    {
                        case '>':
                            this.horizontalBlizzards[currentRow - 1].Add((column, 1));
                            break;
                        case '<':
                            this.horizontalBlizzards[currentRow - 1].Add((column, -1));
                            break;
                        case '^':
                            this.verticalBlizzards[column].Add((currentRow - 1, -1));
                            break;
                        case 'v':
                            this.verticalBlizzards[column].Add((currentRow - 1, 1));
                            break;
                        default:
                            // no-op
                            break;
                    }
                }

                ++currentRow;
            }

            this.height = currentRow - 1;
        }

        public int FindTimeFromEntranceToExit(int startTime)
        {
            return this.FindTimeFrom((0, -1), (this.width - 1, this.height - 1), startTime);
        }

        public int FindTimeFromExitToEntrance(int startTime)
        {
            return this.FindTimeFrom((this.width - 1, this.height), (0, 0), startTime);
        }

        private int FindTimeFrom((int X, int Y) startLocation, (int X, int Y) targetLocation, int startTime)
        {
            // There will be multiple possible moves at any point, so we need the standard BFS to find the path.
            Queue<(int Time, (int X, int Y) Location)> searchQueue = new();
            HashSet<(int Time, (int X, int Y) Location)> seenLocations = new();

            int seenLocationsTimeModulo = this.width * this.height;

            searchQueue.Enqueue((startTime, startLocation));

            while (searchQueue.Count > 0)
            {
                (int time, (int X, int Y) location) = searchQueue.Dequeue();

                ++time;

                if (location == targetLocation)
                {
                    return time;
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
                    if (potentialLocation.X < 0 || potentialLocation.X >= this.width || potentialLocation.Y < 0 || potentialLocation.Y >= this.height)
                    {
                        continue;
                    }

                    // Is there going to be a blizzard there at this point?
                    bool nextLocationWillBeInBlizzard = this.LocationWillBeInHorizontallyMovingBlizzardAtTime(potentialLocation, time)
                        || this.LocationWillBeInVerticallyMovingBlizzardAtTime(potentialLocation, time);

                    if (!nextLocationWillBeInBlizzard)
                    {
                        // This move is good.
                        searchQueue.Enqueue((time, potentialLocation));
                    }
                }

                // Staying still is an option if the current location won't be in the blizzard at the new time.
                bool currentLocationWillBeInBlizzard = this.LocationWillBeInHorizontallyMovingBlizzardAtTime(location, time)
                    || this.LocationWillBeInVerticallyMovingBlizzardAtTime(location, time);
                if (!currentLocationWillBeInBlizzard)
                {
                    searchQueue.Enqueue((time, location));
                }
            }

            throw new Exception();
        }

        private bool LocationWillBeInVerticallyMovingBlizzardAtTime(
            (int X, int Y) potentialLocation,
            int time)
        {
            if (potentialLocation.Y == -1 || potentialLocation.Y == this.height)
            {
                return false;
            }

            int adjustedSteps = time % this.height;

            foreach ((int StartRow, int Direction) verticalBlizzard in this.verticalBlizzards[potentialLocation.X])
            {
                int endRow = (this.height + verticalBlizzard.StartRow + (adjustedSteps * verticalBlizzard.Direction)) % this.height;
                if (endRow == potentialLocation.Y)
                {
                    return true;
                }
            }

            return false;
        }

        private bool LocationWillBeInHorizontallyMovingBlizzardAtTime(
            (int X, int Y) potentialLocation,
            int time)
        {
            if (potentialLocation.Y == -1 || potentialLocation.Y == this.height)
            {
                return false;
            }

            int adjustedSteps = time % this.width;

            foreach ((int StartColumn, int Direction) horizontalBlizzard in this.horizontalBlizzards[potentialLocation.Y])
            {
                int endColumn = (this.width + horizontalBlizzard.StartColumn + (adjustedSteps * horizontalBlizzard.Direction)) % this.width;
                if (endColumn == potentialLocation.X)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
