namespace AdventOfCode.Year2022.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        private static readonly (int XOffset, int YOffset, int ZOffset)[] Offsets = new (int, int, int)[]
        {
            (1, 0, 0),
            (-1, 0, 0),
            (0, 1, 0),
            (0, -1, 0),
            (0, 0, -1),
            (0, 0, 1),
        };

        private enum SpaceType
        {
            Lava,

            Steam,
        }

        public string Solve(string[] input)
        {
            Dictionary<(int X, int Y, int Z), SpaceType> cubes = new();

            foreach (string current in input)
            {
                int separatorIndex = current.IndexOf(',');
                int x = int.Parse(current[..separatorIndex]);
                ReadOnlySpan<char> remainder = current[(separatorIndex + 1) ..];
                separatorIndex = remainder.IndexOf(',');
                int y = int.Parse(remainder[..separatorIndex]);
                int z = int.Parse(remainder[(separatorIndex + 1) ..]);

                cubes.Add((x, y, z), SpaceType.Lava);
            }

            // Now we have the set of cubes making up whatever shape it does... We're going to attack this by
            // finding the bounding box and "filling in" spaces from the outside in, working on the basis
            (int MinX, int MinY, int MinZ, int MaxX, int MaxY, int MaxZ) boundingBox = (
                cubes.Keys.Min(v => v.X) - 1,
                cubes.Keys.Min(v => v.Y) - 1,
                cubes.Keys.Min(v => v.Z) - 1,
                cubes.Keys.Max(v => v.X) + 1,
                cubes.Keys.Max(v => v.Y) + 1,
                cubes.Keys.Max(v => v.Z) + 1);

            // Now we'll start in the corner and recursively fill in the spaces the steam can reach.
            ExpandSteamFrom((boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ), boundingBox, cubes);

            // Now our shape should be surrounded by steam on the outside. This means the next step is to
            // review the lava cubes and count the sides that are next to steam.

            int exposedSurfaces = 0;

            foreach (KeyValuePair<(int X, int Y, int Z), SpaceType> cube in cubes)
            {
                if (cube.Value == SpaceType.Lava)
                {
                    foreach ((int XOffset, int YOffset, int ZOffset) offset in Offsets)
                    {
                        if (cubes.TryGetValue(
                            (cube.Key.X + offset.XOffset, cube.Key.Y + offset.YOffset, cube.Key.Z + offset.ZOffset),
                            out SpaceType spaceType) && spaceType == SpaceType.Steam)
                        {
                            ++exposedSurfaces;
                        }
                    }
                }
            }

            return exposedSurfaces.ToString();
        }

        private static void ExpandSteamFrom(
            (int X, int Y, int Z) startLocation,
            (int MinX, int MinY, int MinZ, int MaxX, int MaxY, int MaxZ) boundingBox,
            Dictionary<(int X, int Y, int Z), SpaceType> cubes)
        {
            Queue<(int X, int Y, int Z)> expansionQueue = new();
            expansionQueue.Enqueue(startLocation);

            while (expansionQueue.Count > 0)
            {
                (int X, int Y, int Z) location = expansionQueue.Dequeue();

                if (location.X < boundingBox.MinX
                    || location.Y < boundingBox.MinY
                    || location.Z < boundingBox.MinZ
                    || location.X > boundingBox.MaxX
                    || location.Y > boundingBox.MaxY
                    || location.Z > boundingBox.MaxZ)
                {
                    // We're outside the area of interest
                    continue;
                }

                if (cubes.ContainsKey(location))
                {
                    // Either we've already filled this space with steam, or it's filled by lava. Either
                    // way we can't expand here.
                    continue;
                }

                // Expand the steam to this location
                cubes.Add(location, SpaceType.Steam);

                foreach ((int XOffset, int YOffset, int ZOffset) offset in Offsets)
                {
                    expansionQueue.Enqueue((location.X + offset.XOffset, location.Y + offset.YOffset, location.Z + offset.ZOffset));
                }
            }
        }
    }
}