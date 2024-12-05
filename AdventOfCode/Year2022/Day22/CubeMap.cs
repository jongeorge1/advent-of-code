namespace AdventOfCode.Year2022.Day22;

using System.Diagnostics;
using System.Linq;

public class CubeMap
{
    public const char OpenTile = '.';
    public const char SolidWall = '#';
    public const char Void = ' ';

    public static readonly (int dX, int dY)[] DirectionOffsets = new[]
    {
        (1, 0),
        (0, 1),
        (-1, 0),
        (0, -1),
    };

    public CubeMap(string[] map)
    {
        CubeFaceDescriptor.FaceSize = map.Length > 12 ? 50 : 4;

        // To understand the cube, we first need to work out how the net is structured.
        // Imagine the input being split into a grid with each square containing 50 map
        // spaces. We need to work out which spaces in that grid contain cube faces.
        int gridWidth = map.Max(x => x.Length) / CubeFaceDescriptor.FaceSize;
        int gridHeight = map.Length / CubeFaceDescriptor.FaceSize;

        int cubeFacesDiscovered = 0;
        var cubeFaces = new CubeFaceDescriptor[6];

        for (int gridRow = 0; gridRow < gridHeight; ++gridRow)
        {
            for (int gridColumn = 0; gridColumn < gridWidth; ++gridColumn)
            {
                string targetRow = map[gridRow * CubeFaceDescriptor.FaceSize];

                if (targetRow.Length > (gridColumn * CubeFaceDescriptor.FaceSize) && targetRow[gridColumn * CubeFaceDescriptor.FaceSize] != Void)
                {
                    cubeFaces[cubeFacesDiscovered++] = new CubeFaceDescriptor
                    {
                        Id = cubeFacesDiscovered,
                        CubeGridRow = gridRow,
                        CubeGridColumn = gridColumn,
                        MapGridXBounds = (gridColumn * CubeFaceDescriptor.FaceSize, (gridColumn * CubeFaceDescriptor.FaceSize) + CubeFaceDescriptor.FaceSize - 1),
                        MapGridYBounds = (gridRow * CubeFaceDescriptor.FaceSize, (gridRow * CubeFaceDescriptor.FaceSize) + CubeFaceDescriptor.FaceSize - 1),
                    };
                }
            }
        }

        // Now we have the descriptors, link them together. This step is kind of like folding up the cube and is the hardest bit; we
        // we need to also determine the direction change that will apply as we transition from one face to the next.
        // First do the directly connected faces.
        foreach (CubeFaceDescriptor cubeFace in cubeFaces)
        {
            for (int direction = 0; direction < DirectionOffsets.Length; ++direction)
            {
                if (cubeFace.ConnectedFaces[direction] is null)
                {
                    CubeFaceDescriptor? directlyConnectedFace = cubeFaces
                        .SingleOrDefault(target => target.CubeGridColumn == cubeFace.CubeGridColumn + DirectionOffsets[direction].dX && target.CubeGridRow == cubeFace.CubeGridRow + DirectionOffsets[direction].dY);

                    if (directlyConnectedFace is not null)
                    {
                        cubeFace.ConnectedFaces[direction] = directlyConnectedFace;
                        cubeFace.FaceTraversalDirectionChanges[direction] = 0;

                        directlyConnectedFace.ConnectedFaces[ChangeDirection(direction, 2)] = cubeFace;
                        directlyConnectedFace.FaceTraversalDirectionChanges[ChangeDirection(direction, 2)] = 0;
                    }
                }
            }
        }

        // Now the remaining scenarios. There are only 11 ways you can lay out a cube net, so there are only so many scenarios
        // we need to consider.
        bool changed = true;
        while (changed)
        {
            changed = false;
            foreach (CubeFaceDescriptor cubeFace in cubeFaces)
            {
                for (int direction = 0; direction < 4; ++direction)
                {
                    if (cubeFace.ConnectedFaces[direction] is null)
                    {
                        // The face isn't connected. However, if either of the faces on the
                        // adjacent sides are, and they have further connections in this direction,
                        // we can join these up. We need to check both the left and right sides
                        // separately. To make thinking about this easier, the comments will pretend
                        // that our "direction" is "right", and the adjacent sides are "up" and "down".
                        int down = ChangeDirection(direction, 1);
                        int up = ChangeDirection(direction, -1);

                        // Slightly more problematic is that the connected face might require an orientation
                        // change so it's not as simple as down-right.
                        if (cubeFace.ConnectedFaces[down] is not null)
                        {
                            int nextDirection = ChangeDirection(direction, cubeFace.FaceTraversalDirectionChanges[down]);
                            CubeFaceDescriptor? targetFace = cubeFace.ConnectedFaces[down].ConnectedFaces[nextDirection];
                            if (targetFace is not null)
                            {
                                int combinedTraversalDirectionChanges = cubeFace.FaceTraversalDirectionChanges[down] + cubeFace.ConnectedFaces[down].FaceTraversalDirectionChanges[nextDirection];
                                cubeFace.ConnectedFaces[direction] = targetFace;
                                cubeFace.FaceTraversalDirectionChanges[direction] = combinedTraversalDirectionChanges + 1;
                                int targetFaceConnectionEdge = ChangeDirection(up, combinedTraversalDirectionChanges);
                                Debug.Assert(targetFace.ConnectedFaces[targetFaceConnectionEdge] is null);
                                targetFace.ConnectedFaces[targetFaceConnectionEdge] = cubeFace;
                                targetFace.FaceTraversalDirectionChanges[targetFaceConnectionEdge] = -combinedTraversalDirectionChanges - 1;
                                changed = true;
                            }
                        }
                        else if (cubeFace.ConnectedFaces[direction] is null && cubeFace.ConnectedFaces[up] is not null)
                        {
                            int nextDirection = ChangeDirection(direction, cubeFace.FaceTraversalDirectionChanges[up]);
                            CubeFaceDescriptor? targetFace = cubeFace.ConnectedFaces[up].ConnectedFaces[nextDirection];
                            if (targetFace is not null)
                            {
                                int combinedTraversalDirectionChanges = cubeFace.FaceTraversalDirectionChanges[up] + cubeFace.ConnectedFaces[up].FaceTraversalDirectionChanges[nextDirection];
                                cubeFace.ConnectedFaces[direction] = targetFace;
                                cubeFace.FaceTraversalDirectionChanges[direction] = combinedTraversalDirectionChanges - 1;
                                int targetFaceConnectionEdge = ChangeDirection(down, combinedTraversalDirectionChanges);
                                Debug.Assert(targetFace.ConnectedFaces[targetFaceConnectionEdge] is null);
                                targetFace.ConnectedFaces[targetFaceConnectionEdge] = cubeFace;
                                targetFace.FaceTraversalDirectionChanges[targetFaceConnectionEdge] = 1 - combinedTraversalDirectionChanges;
                                changed = true;
                            }
                        }
                    }
                }
            }
        }

        this.CubeFaces = cubeFaces;
    }

    public CubeFaceDescriptor[] CubeFaces { get; }

    public static int ChangeDirection(int currentDirection, int change)
    {
        return (currentDirection + change + 4) % 4;
    }
}
